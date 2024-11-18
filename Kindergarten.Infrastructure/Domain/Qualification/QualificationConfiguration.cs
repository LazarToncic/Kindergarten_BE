using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastructure.Domain.Qualification;

public class QualificationConfiguration : IEntityTypeConfiguration<Kindergarten.Domain.Entities.Qualification>
{
    public void Configure(EntityTypeBuilder<Kindergarten.Domain.Entities.Qualification> builder)
    {
        builder.ToTable("Qualification");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(30);
        
        builder.Property(q => q.Description)
            .HasMaxLength(250);

        builder.HasMany(x => x.EmployeeQualifications)
            .WithOne(x => x.Qualification)
            .HasForeignKey(x => x.QualificationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.QualificationType)
            .WithMany(x => x.Qualifications)
            .HasForeignKey(x => x.QualificationTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}