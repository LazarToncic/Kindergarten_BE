using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastructure.Domain.ParentConfig;

public class ParentConfiguration : IEntityTypeConfiguration<Parent>
{
    public void Configure(EntityTypeBuilder<Parent> builder)
    {
        builder.ToTable("Parent");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.VerifiedAt).IsRequired();

        builder.HasOne(x => x.User)
            .WithOne(x => x.Parent)
            .HasForeignKey<Parent>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.ParentChildren)
            .WithOne(x => x.Parent)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}