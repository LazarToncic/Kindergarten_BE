using System.Reflection;
using Kindergarten.Application.Common.Interfaces;
using Kindergarten.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Kindergarten.Infrastructure.Context;

public class KindergartenDbContext(DbContextOptions<KindergartenDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, 
    ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>(options), IKindergartenDbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=root;Database=Kindergarten");
    }
}