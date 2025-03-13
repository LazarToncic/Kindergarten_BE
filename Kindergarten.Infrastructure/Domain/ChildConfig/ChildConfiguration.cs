using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastructure.Domain.ChildConfig;

public class ChildConfiguration : IEntityTypeConfiguration<Child>
{
    public void Configure(EntityTypeBuilder<Child> builder)
    {
        builder.ToTable("Child");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(30);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(30);
        builder.Property(x => x.YearOfBirth).IsRequired();
        
        builder.HasMany(x => x.ParentChildren)
            .WithOne(x => x.Child)
            .HasForeignKey(x => x.ChildId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ChildAllergies)
            .WithOne(x => x.Child)
            .HasForeignKey(x => x.ChildId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ChildMedicalConditions)
            .WithOne(x => x.Child)
            .HasForeignKey(x => x.ChildId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}