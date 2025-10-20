using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserC.Domain.Entities;

namespace UserC.Infrastructure.Persistence.Config;

public class UserConfig : 
    IEntityTypeConfiguration<User>,
    IEntityTypeConfiguration<Favorite>,
    IEntityTypeConfiguration<Follow>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users").HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Avatar).HasColumnName("avatar");
        builder.Property(x => x.DisplayName).HasColumnName("display_name");
        builder.Property(x => x.Banner).HasColumnName("banner");

        //builder.HasMany(x => x.Favorites).WithOne().HasForeignKey(x => x.UserId);
    }

    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        builder.ToTable("favorites").HasKey(x => new { x.UserId, x.ItemId });
        
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.ItemId).HasColumnName("item_id");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");

        builder.HasOne(x => x.Item).WithMany().HasForeignKey(x => x.ItemId);
        builder.HasOne<User>().WithMany(x => x.Favorites).HasForeignKey(x => x.UserId);
    }

    public void Configure(EntityTypeBuilder<Follow> builder)
    {
        builder.ToTable("follows").HasKey(x => new { x.FollowerId, x.FollowingId });
        
        builder.Property(x => x.FollowerId).HasColumnName("follower_id");
        builder.Property(x => x.FollowingId).HasColumnName("following_id");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");

        builder.HasOne(x => x.Following).WithMany().HasForeignKey(x => x.FollowingId);
        builder.HasOne<User>().WithMany(x => x.Followings).HasForeignKey(x => x.FollowerId);
    }
}