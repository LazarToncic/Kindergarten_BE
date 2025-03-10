using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastructure.Domain.AllergyConfig;

public class AllergyConfiguration : IEntityTypeConfiguration<Allergy>
{
    public void Configure(EntityTypeBuilder<Allergy> builder)
    {
        builder.ToTable("Allergy");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.HasMany(x => x.ChildAllergies)
            .WithOne(x => x.Allergy)
            .HasForeignKey(x => x.AllergyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}