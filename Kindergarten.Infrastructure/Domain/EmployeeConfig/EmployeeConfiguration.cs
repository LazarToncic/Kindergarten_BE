using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastructure.Domain.EmployeeConfig;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.UserId)
            .IsRequired();

        builder.HasOne(e => e.User) 
            .WithOne(u => u.Employee) 
            .HasForeignKey<Employee>(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.EmployeePosition)
            .WithMany(x => x.Employees)
            .HasForeignKey(x => x.EmployeePositionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Kindergarten)
            .WithMany(x => x.Employees)
            .HasForeignKey(x => x.KindergartenId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.DepartmentEmployees)
            .WithOne(x => x.Employee)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Salaries)
            .WithOne(x => x.Employee)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.EmployeeQualifications)
            .WithOne(x => x.Employee)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.HiredDate)
            .IsRequired();
    }
}