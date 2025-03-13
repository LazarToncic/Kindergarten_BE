using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastructure.Domain.MedicalConditionConfig;

public class MedicalConditionConfiguration : IEntityTypeConfiguration<MedicalCondition>
{
    public void Configure(EntityTypeBuilder<MedicalCondition> builder)
    {
        builder.ToTable("MedicalCondition");
        builder.HasKey(m => m.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(30);

        builder.HasMany(x => x.ChildMedicalConditions)
            .WithOne(m => m.MedicalCondition)
            .HasForeignKey(x => x.MedicalConditionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}