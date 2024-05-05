
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Reflection;
using Mosbi.Domain.Entities.Membership;
using Mosbi.Domain.Entities;
using Mosbi.Application.Abstracts.Common.Interfaces;

namespace Mosbi.Infrastructure.Persistance;

public class MosbiDbContext : IdentityDbContext<AppUser, AppRole, int, AppUserClaim, AppUserRole,
        AppUserLogin, AppRoleClaim, AppUserToken>, IMosbiDbContext
{
    public MosbiDbContext(DbContextOptions<MosbiDbContext> options) : base(options) { }

    public DbSet<Category> Categories => Set <Category>();
   

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
