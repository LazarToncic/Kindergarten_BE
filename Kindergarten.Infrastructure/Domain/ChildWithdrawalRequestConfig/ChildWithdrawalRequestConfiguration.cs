using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kindergarten.Infrastructure.Domain.ChildWithdrawalRequestConfig;

public class ChildWithdrawalRequestConfiguration : IEntityTypeConfiguration<ChildWithdrawalRequest>
{
    public void Configure(EntityTypeBuilder<ChildWithdrawalRequest> builder)
    {
        builder.ToTable("ChildWithdrawalRequest");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.RequestedAt)
            .IsRequired();

        builder.Property(r => r.RequestedByUserId)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(r => r.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(r => r.Reason)
            .HasMaxLength(400);

        builder.Property(r => r.ReviewedByUserId)
            .HasMaxLength(450);

        builder.Property(r => r.ReviewedAt);
        
        builder.HasOne(x => x.Child)
            .WithMany(x => x.WithdrawalRequests)
            .HasForeignKey(x => x.ChildId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(r => r.RequestedByUser)      
            .WithMany()                        
            .HasForeignKey(r => r.RequestedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.ReviewedByUser)   
            .WithMany()
            .HasForeignKey(r => r.ReviewedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}