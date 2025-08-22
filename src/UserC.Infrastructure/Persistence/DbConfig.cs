using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserC.Domain.Entities;
using UserC.Infrastructure.Models;

namespace UserC.Infrastructure.Persistence;

public class DbConfig :
    IEntityTypeConfiguration<Brand>,
    IEntityTypeConfiguration<Category>,
    IEntityTypeConfiguration<Item>,
    IEntityTypeConfiguration<SKU>,
    IEntityTypeConfiguration<Inventory>,
    IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("brands").HasKey(x => x.Id);
        
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Note).HasColumnName("note");
        builder.Property(x => x.Alias).HasColumnName("alias");
        builder.Property(x => x.Logo).HasColumnName("logo_url");
    }

    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories").HasKey(x => x.Id);
        
        builder.Property(x => x.Id).HasColumnName("id").UseIdentityAlwaysColumn();
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Note).HasColumnName("note");
        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.Level).HasColumnName("level");
        builder.Property(x => x.ParentId).HasColumnName("parent_id"); 
        
        builder.HasOne<Category>(x => x.Parent).WithMany(x => x.Children).HasForeignKey(x => x.ParentId);

        builder.HasMany(x => x.Brands)
            .WithMany()
            .UsingEntity<CategoriesBrands>(
                l => l.HasOne<Brand>(e => e.Brand).WithMany().HasForeignKey(e => e.BrandId),
                r => r.HasOne<Category>(e => e.Category).WithMany().HasForeignKey(e => e.CategoryId),
                b =>
                {
                    b.ToTable("categories_brands");
                    b.HasKey(e => new { e.CategoryId, e.BrandId });
                    b.Property(x => x.BrandId).HasColumnName("brand_id");
                    b.Property(x => x.CategoryId).HasColumnName("category_id");
                });
    }

    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("items").HasKey(x => x.Id);
        
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.Property(x => x.SpuId).HasColumnName("spu_id");
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Category1Id).HasColumnName("category1_id");
        builder.Property(x => x.Category2Id).HasColumnName("category2_id");
        builder.Property(x => x.Category3Id).HasColumnName("category3_id");
        builder.Property(x => x.BrandId).HasColumnName("brand_id");
        builder.Property(x => x.IsService).HasColumnName("is_service");
        builder.Property(x => x.Albums).HasColumnName("albums");
        builder.Property(x => x.Specs).HasColumnName("spec").HasColumnType("jsonb");

        builder.HasMany(x => x.Skus).WithOne().HasForeignKey("item_id");
        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);

    }
    
    public void Configure(EntityTypeBuilder<SKU> builder)
    {
        builder.ToTable("skus").HasKey(x => x.Id);
        
        builder.Property(x => x.Id).HasColumnName("id").UseIdentityAlwaysColumn();
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Photo).HasColumnName("photo");
        builder.Property(x => x.Specs).HasColumnName("specs").HasColumnType("jsonb");
        builder.Property(x => x.Price).HasColumnName("price");

        builder.HasMany(x => x.Inventories).WithOne().HasForeignKey("sku_id");
    }
    
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("inventories").HasKey(x => x.Id);
        
        builder.Property(x => x.Id).HasColumnName("id").UseIdentityAlwaysColumn();
        builder.Property(x => x.AvailableStock).HasColumnName("available_stock");
        builder.Property(x => x.AllocatedStock).HasColumnName("allocated_stock");
        builder.Property(x => x.ReservedStock).HasColumnName("reserved_stock");
        builder.Property(x => x.LowStackThreshold).HasColumnName("low_stock_threshold");
        builder.Property(x => x.LastUpdated).HasColumnName("last_updated");
    }

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users").HasKey(x => x.Id);
        
        builder.Property(x => x.Id).HasColumnName("id").UseIdentityAlwaysColumn();
        builder.Property(x => x.Avatar).HasColumnName("avatar");
        builder.Property(x => x.DisplayName).HasColumnName("display_name");
        builder.Property(x => x.Banner).HasColumnName("banner");
    }
}