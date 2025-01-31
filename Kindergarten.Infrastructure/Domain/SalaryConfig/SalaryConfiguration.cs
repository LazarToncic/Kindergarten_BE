using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastructure.Domain.SalaryConfig;

public class SalaryConfiguration : IEntityTypeConfiguration<Salary>
{
    public void Configure(EntityTypeBuilder<Salary> builder)
    {
        builder.ToTable("Salaries");

        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Amount)
            .IsRequired();

        builder.Property(s => s.EffectiveFrom)
            .IsRequired();

        builder.Property(s => s.Currency)
            .IsRequired();

        builder.Property(s => s.EmployeePosition)
            .IsRequired();

        builder.HasOne(s => s.Employee)
            .WithMany(e => e.Salaries)
            .HasForeignKey(s => s.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}