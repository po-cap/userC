using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserC.Domain.Entities.Items;

namespace UserC.Infrastructure.Persistence.Config;

public class ItemConfig : 
    IEntityTypeConfiguration<Category>,
    IEntityTypeConfiguration<CAttribute>,
    IEntityTypeConfiguration<Item>,
    IEntityTypeConfiguration<Album>,
    IEntityTypeConfiguration<SKU>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories").HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.ParentId).HasColumnName("parent_id");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Level).HasColumnName("level");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at");
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");

        builder.HasOne(x => x.Parent).WithMany(x => x.Children).HasForeignKey(x => x.ParentId);
    }

    public void Configure(EntityTypeBuilder<CAttribute> builder)
    {
        builder.ToTable("attributes").HasKey(x => x.CategoryId);

        builder.Property(x => x.CategoryId).HasColumnName("category_id");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Values).HasColumnName("values");

        builder.HasOne<Category>().WithMany().HasForeignKey(x => x.CategoryId);
    }

    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("items").HasKey(x => x.Id);
        
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.CategoryId).HasColumnName("category_id");
        builder.Property(x => x.DeListed).HasColumnName("delisted");
        builder.Property(x => x.Extra).HasColumnName("extra").HasColumnType("jsonb");
        builder.Property(x => x.ShippingFee).HasColumnName("shipping_fee");
        
        builder.HasOne<Category>().WithMany().HasForeignKey(x => x.CategoryId);
        builder.HasOne<Album>(x => x.Album).WithOne().HasForeignKey<Album>(x => x.ItemId);
    }

    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.ToTable("items_album").HasKey(x => x.ItemId);
        
        builder.Property(x => x.ItemId).HasColumnName("item_id");
        builder.Property(x => x.Assets).HasColumnName("assets");
        builder.Property(x => x.IsVideo).HasColumnName("is_video");
    }

    public void Configure(EntityTypeBuilder<SKU> builder)
    {
        builder.ToTable("skus").HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.ItemId).HasColumnName("item_id");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Metadata).HasColumnName("metadata");
        builder.Property(x => x.Price).HasColumnName("price");
        builder.Property(x => x.AvailableStock).HasColumnName("available_stock");
        builder.Property(x => x.AllocatedStock).HasColumnName("allocated_stock");
        
        builder.HasOne<Item>().WithMany(x => x.Skus).HasForeignKey(x => x.ItemId);
    }
}