using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastructure.Domain.ChildDepartmentConfig;

public class ChildDepartmentConfiguration : IEntityTypeConfiguration<ChildDepartment>
{
    public void Configure(EntityTypeBuilder<ChildDepartment> builder)
    {
        builder.ToTable("ChildDepartmentAssignments");
        builder.HasKey(cda => cda.Id);
        
        builder.HasOne(cda => cda.Child)
            .WithMany(c => c.DepartmentAssignments)
            .HasForeignKey(cda => cda.ChildId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(cda => cda.Department)
            .WithMany(d => d.ChildDepartmentAssignments)
            .HasForeignKey(cda => cda.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(cda => cda.AssignedAt)
            .IsRequired();
        
        builder.Property(cda => cda.AssignedByUserId)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(cda => cda.IsActive)
            .IsRequired();
    }
}