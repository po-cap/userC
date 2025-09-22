using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserC.Domain.Entities.Rating;

namespace UserC.Infrastructure.Persistence.Config;

public class RatingConfig : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("reviews").HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.OrderId).HasColumnName("oder_id");
        builder.Property(x => x.ReviewerAvatar).HasColumnName("reviewer_avatar");
        builder.Property(x => x.ReviewerDisplayName).HasColumnName("reviewer_display_name");
        builder.Property(x => x.Rating).HasColumnName("rating");
        builder.Property(x => x.Comment).HasColumnName("comment");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");
    }
}