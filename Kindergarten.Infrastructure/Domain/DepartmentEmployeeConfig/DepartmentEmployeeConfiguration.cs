using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastructure.Domain.DepartmentEmployeeConfig;

public class DepartmentEmployeeConfiguration : IEntityTypeConfiguration<DepartmentEmployee>
{
    public void Configure(EntityTypeBuilder<DepartmentEmployee> builder)
    {
        builder.HasKey(de => new { de.DepartmentId, de.EmployeeId });

        builder.HasOne(de => de.Department)
            .WithMany(d => d.DepartmentEmployees)
            .HasForeignKey(de => de.DepartmentId);

        builder.HasOne(de => de.Employee)
            .WithMany(e => e.DepartmentEmployees)
            .HasForeignKey(de => de.EmployeeId);

        builder.Property(de => de.RoleInDepartment)
            .IsRequired()
            .HasMaxLength(30);
    }
}