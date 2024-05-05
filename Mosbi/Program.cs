using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Mosbi.Application;
using Mosbi.Application.Extensions;
using Mosbi.Infrastructure;
using Mosbi.Infrastructure.Identity.Providers;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.ListenAnyIP(5000);
//});


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod()
    );
});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var assemblies = AppDomain.CurrentDomain
    .GetAssemblies()
    .ToArray();
builder.Services.AddMediatR(assemblies);
var types = typeof(Program).Assembly.GetTypes();
builder.Services.AddValidatorsFromAssemblies(assemblies, ServiceLifetime.Singleton);
AppClaimProvider.principals = types
    .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && t.IsDefined(typeof(AuthorizeAttribute), true))
    .SelectMany(t => t.GetCustomAttributes<AuthorizeAttribute>())
    .Union(
    types
    .Where(t => typeof(ControllerBase).IsAssignableFrom(t))
    .SelectMany(type => type.GetMethods())
    .Where(method => method.IsPublic
     && !method.IsDefined(typeof(NonActionAttribute), true)
     && method.IsDefined(typeof(AuthorizeAttribute), true))
     .SelectMany(t => t.GetCustomAttributes<AuthorizeAttribute>())
    )
    .Where(a => !string.IsNullOrWhiteSpace(a.Policy))
    .SelectMany(a => a.Policy.Split(new[] { "," }, System.StringSplitOptions.RemoveEmptyEntries))
.Distinct()
.ToArray();

builder.Services.AddAuthorization(cfg =>
{

    foreach (string principal in AppClaimProvider.principals)
    {
        cfg.AddPolicy(principal, p =>
        {
            p.RequireAssertion(handler =>
            {
                return handler.User.HasAccess(principal);

            });
        });
    }
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Mosbi API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

app.UseCors("CorsPolicy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "images")),
    RequestPath = "/images"
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
