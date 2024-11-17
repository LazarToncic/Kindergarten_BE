using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastructure.Domain.KindergartenDepartmentConfig;

public class KindergartenDepartmentConfiguration : IEntityTypeConfiguration<KindergartenDepartment>
{
    public void Configure(EntityTypeBuilder<KindergartenDepartment> builder)
    {
        builder.HasKey(kd => new { kd.DepartmentId, kd.KindergartenId });
        
        builder.HasOne(kd => kd.Department)
            .WithMany(d => d.KindergartenDepartments)
            .HasForeignKey(kd => kd.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(kd => kd.Kindergarten)
            .WithMany(k => k.KindergartenDepartment) 
            .HasForeignKey(kd => kd.KindergartenId)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}