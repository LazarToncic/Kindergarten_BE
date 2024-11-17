using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastructure.Domain.EmployeeQualification;

public class EmployeeQualificationConfiguration : IEntityTypeConfiguration<Kindergarten.Domain.Entities.EmployeeQualification>
{
    public void Configure(EntityTypeBuilder<Kindergarten.Domain.Entities.EmployeeQualification> builder)
    {
        builder.ToTable("EmployeeQualifications");
        
        builder.HasKey(eq => eq.Id);

        builder.Property(eq => eq.QualificationObtained)
            .IsRequired();
        
        builder.HasOne(eq => eq.Employee)
            .WithMany(e => e.EmployeeQualifications)
            .HasForeignKey(eq => eq.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(eq => eq.Qualification)
            .WithMany(q => q.EmployeeQualifications)
            .HasForeignKey(eq => eq.QualificationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}