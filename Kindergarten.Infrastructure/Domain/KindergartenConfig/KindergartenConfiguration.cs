using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastructure.Domain.KindergartenConfig;

public class KindergartenConfiguration : IEntityTypeConfiguration<Kindergarten.Domain.Entities.Kindergarten>
{
    public void Configure(EntityTypeBuilder<Kindergarten.Domain.Entities.Kindergarten> builder)
    {
        builder.ToTable("Kindergarten");

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Address).IsRequired();
        builder.Property(x => x.City).IsRequired();
        builder.Property(x => x.ContactPhone).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
    }
}