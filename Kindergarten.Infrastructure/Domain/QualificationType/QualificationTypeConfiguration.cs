using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastructure.Domain.QualificationType;

public class QualificationTypeConfiguration : IEntityTypeConfiguration<Kindergarten.Domain.Entities.QualificationType>
{
    public void Configure(EntityTypeBuilder<Kindergarten.Domain.Entities.QualificationType> builder)
    {
        builder.ToTable("QualificationType");
        
        builder.HasKey(qt => qt.Id); 
        
        builder.Property(qt => qt.Name)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasMany(x => x.Qualifications)
            .WithOne(x => x.QualificationType)
            .HasForeignKey(x => x.QualificationTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}