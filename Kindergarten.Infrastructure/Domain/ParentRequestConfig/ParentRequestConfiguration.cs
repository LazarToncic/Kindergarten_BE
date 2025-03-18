using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastructure.Domain.ParentRequestConfig;

public class ParentRequestConfiguration : IEntityTypeConfiguration<ParentRequest>
{
    public void Configure(EntityTypeBuilder<ParentRequest> builder)
    {
        builder.ToTable("ParentRequest");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.NumberOfChildren).IsRequired();
        builder.Property(x => x.AdditionalInfo).HasMaxLength(300);
        builder.Property(x => x.IsOnlineApproved).IsRequired();
        builder.Property(x => x.OnlineApprovedByUserId).HasMaxLength(255);
        builder.Property(x => x.IsInPersonApproved).IsRequired();
        builder.Property(x => x.InPersonApprovedByUserId).HasMaxLength(255);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.ApprovedAt).IsRequired(false);
        builder.Property(x => x.PreferredKindergarten).IsRequired();
        builder.Property(x => x.ParentRole)
            .HasConversion<string>()
            .IsRequired();
        
        builder.Property(x => x.ChildrenJson)
            .HasColumnType("jsonb")
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.ParentRequests)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // ako zelis da dodas u withMany moras u ApplicationUser da napravis 2 nove liste za ovo
        builder.HasOne(x => x.OnlineApprovedByUser)
            .WithMany()
            .HasForeignKey(x => x.OnlineApprovedByUserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.InPersonApprovedByUser)
            .WithMany()
            .HasForeignKey(x => x.InPersonApprovedByUserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}