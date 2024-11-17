using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastructure.Domain.KindergartenConfig;

public class KindergartenConfiguration : IEntityTypeConfiguration<Kindergarten.Domain.Entities.Kindergarten>
{
    public void Configure(EntityTypeBuilder<Kindergarten.Domain.Entities.Kindergarten> builder)
    {
        builder.ToTable("Kindergarten");

        builder.HasKey(k => k.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(40);
        builder.Property(x => x.City).IsRequired().HasMaxLength(30);
        builder.Property(x => x.ContactPhone).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasMany(x => x.KindergartenDepartment)
            .WithOne(x => x.Kindergarten)
            .HasForeignKey(x => x.KindergartenId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Employees)
            .WithOne(x => x.Kindergarten)
            .HasForeignKey(x => x.KindergartenId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}