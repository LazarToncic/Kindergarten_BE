using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastructure.Domain.ChildAllergyConfig;

public class ChildAllergyConfiguration : IEntityTypeConfiguration<ChildAllergy>
{
    public void Configure(EntityTypeBuilder<ChildAllergy> builder)
    {
        builder.ToTable("ChildAllergies");

        builder.HasKey(ca => new { ca.ChildId, ca.AllergyId });

        builder.HasOne(x => x.Child)
            .WithMany(x => x.ChildAllergies)
            .HasForeignKey(x => x.ChildId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Allergy)
            .WithMany(x => x.ChildAllergies)
            .HasForeignKey(x => x.AllergyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}