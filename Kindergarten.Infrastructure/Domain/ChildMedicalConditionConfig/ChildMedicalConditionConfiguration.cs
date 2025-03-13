using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastructure.Domain.ChildMedicalConditionConfig;

public class ChildMedicalConditionConfiguration : IEntityTypeConfiguration<ChildMedicalCondition>
{
    public void Configure(EntityTypeBuilder<ChildMedicalCondition> builder)
    {
        builder.ToTable("ChildMedicalCondition");

        builder.HasKey(x => new { x.ChildId, x.MedicalConditionId });

        builder.HasOne(x => x.Child)
            .WithMany(x => x.ChildMedicalConditions)
            .HasForeignKey(x => x.ChildId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.MedicalCondition)
            .WithMany(x => x.ChildMedicalConditions)
            .HasForeignKey(x => x.MedicalConditionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}