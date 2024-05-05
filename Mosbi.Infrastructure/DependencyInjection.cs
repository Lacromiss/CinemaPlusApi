using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Mosbi.Application.Abstracts.Common.Interfaces;
using Mosbi.Domain.Entities.Membership;
using Mosbi.Infrastructure.Concretes.Common;
using Mosbi.Infrastructure.Identity.Providers;
using Mosbi.Infrastructure.Persistance;
using Mosbi.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosbi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddScoped<UserManager<AppUser>>();
            services.AddScoped<SignInManager<AppUser>>();
            services.AddScoped<RoleManager<AppRole>>();

            services.AddScoped<IMosbiDbContext>(provider => provider.GetRequiredService<MosbiDbContext>());
            services.AddDbContext<MosbiDbContext>(options =>
                 options.UseNpgsql(configuration.GetConnectionString("cString")));
            services.AddIdentity<AppUser, AppRole>()
                            .AddEntityFrameworkStores<MosbiDbContext>()
                            .AddDefaultTokenProviders()
                            .AddErrorDescriber<MosbiIdentityErrorDescriber>();

            services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["jwt:issuer"],
                    ValidAudience = configuration["jwt:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"])),
                    ClockSkew = TimeSpan.FromDays(1),
                    LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                    {
                        return expires >= DateTime.UtcNow;
                    }
                };
            });
            services.Configure<CryptoServiceOptions>(cfg =>
            {
                configuration.GetSection("cryptograpy").Bind(cfg);
            });
            services.AddSingleton<ICryptoService, CryptoService>();

            services.Configure<EmailServiceOptions>(cfg =>
            {
                configuration.GetSection("emailAccount").Bind(cfg);
            });
            services.AddSingleton<IEmailService, EmailService>();

            services.Configure<TokenServiceOptions>(cfg =>
            {
                configuration.GetSection("jwt").Bind(cfg);
                cfg.DurationMinutes = (int)TimeSpan.FromHours(3000).TotalMinutes;
            });
            services.AddSingleton<ITokenService, TokenService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IClaimsTransformation, AppClaimProvider>();

            return services;
        }
    }
}
