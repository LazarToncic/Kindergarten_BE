using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastructure.Domain.DepartmentConfig;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Departments");
        
        builder.HasKey(x => x.Id);
        builder.Property(k => k.Name)
            .IsRequired()
            .HasMaxLength(30); 

        builder.Property(d => d.AgeGroup)
            .IsRequired();

        builder.Property(d => d.Capacity)
            .IsRequired();
        
        builder.Property(d => d.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.KindergartenDepartments)
            .WithOne(x => x.Department)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.DepartmentEmployees)
            .WithOne(x => x.Department)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}