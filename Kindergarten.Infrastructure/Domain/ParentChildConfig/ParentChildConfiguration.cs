using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastructure.Domain.ParentChildConfig;

public class ParentChildConfiguration : IEntityTypeConfiguration<ParentChild>
{
    public void Configure(EntityTypeBuilder<ParentChild> builder)
    {
        builder.ToTable("ParentChild");
        builder.HasKey(x => new { x.ParentId, x.ChildId });
        
        builder.HasOne(x => x.Parent)
            .WithMany(x => x.ParentChildren)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Child)
            .WithMany(x => x.ParentChildren)
            .HasForeignKey(x => x.ChildId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}