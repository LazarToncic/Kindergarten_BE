using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastructure.Domain.EmployeePositionConfig;

public class EmployeePositionConfiguration : IEntityTypeConfiguration<EmployeePosition>
{
    public void Configure(EntityTypeBuilder<EmployeePosition> builder)
    {
        builder.ToTable("EmployeePositions");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired() 
            .HasMaxLength(50);

        builder.HasMany(x => x.Employees)
            .WithOne(x => x.EmployeePosition)
            .HasForeignKey(x => x.EmployeePositionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}