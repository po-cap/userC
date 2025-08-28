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
    IEntityTypeConfiguration<User>,
    IEntityTypeConfiguration<Order>
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
        builder.Property(x => x.ShippingFee).HasColumnName("shipping_fee");

        builder.HasMany(x => x.Skus).WithOne().HasForeignKey(x => x.ItemId);
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
        builder.Property(x => x.AvailableStock).HasColumnName("available_stock");
        builder.Property(x => x.AllocatedStock).HasColumnName("allocated_stock");
        builder.Property(x => x.ItemId).HasColumnName("item_id");

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

    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("transactions").HasKey(x => x.Id);
        
        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.BuyerId).HasColumnName("buyer_id");
        builder.Property(x => x.SellerId).HasColumnName("seller_id");
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.BuyerId);
        builder.HasOne<User>().WithMany().HasForeignKey(x => x.SellerId);

        builder.Property(x => x.OrderAmount).HasColumnName("order_amount");
        builder.Property(x => x.DiscountAmount).HasColumnName("discount_amount");
        builder.Property(x => x.TaxAmount).HasColumnName("tax_amount");
        builder.Property(x => x.ShippingFee).HasColumnName("shipping_fee");
        builder.Property(x => x.TotalAmount).HasColumnName("total_amount");
        
        builder.Property(x => x.ShippingProvider).HasColumnName("shipping_provider");
        builder.Property(x => x.TrackingNumber).HasColumnName("tracking_number");

        builder.Property(x => x.BuyerNote).HasColumnName("buyer_note");
        builder.Property(x => x.SellerNote).HasColumnName("seller_note");

        builder.Property(x => x.Status).HasColumnName("status");
        builder.Property(x => x.PaidAt).HasColumnName("paid_at");
        builder.Property(x => x.ShippedAt).HasColumnName("shipped_at");
        builder.Property(x => x.DeliveredAt).HasColumnName("delivered_at");
        builder.Property(x => x.CompletedAt).HasColumnName("completed_at");
        builder.Property(x => x.CancelledAt).HasColumnName("cancelled_at");
        builder.Property(x => x.RefundAt).HasColumnName("refunded_at");
        
        builder.Property(x => x. ItemId).HasColumnName("item_id");
        builder.HasOne<Item>().WithMany().HasForeignKey(x => x.ItemId);
        builder.Property(x => x.SkuId).HasColumnName("sku_id");
        builder.HasOne<SKU>().WithMany().HasForeignKey(x => x.SkuId);
        builder.Property(x => x.ItemSpec).HasColumnName("item_spec").HasColumnType("jsonb");
        builder.Property(x => x.SkuSpec).HasColumnName("sku_spec").HasColumnType("jsonb");
        builder.Property(x => x.quantity).HasColumnName("quantity");
        
        
        builder.Property(x => x. RefundAmount).HasColumnName("refund_amount");
        builder.Property(x => x. RefundQuantity).HasColumnName("refund_quantity");
        
        builder.Property(x => x. RecipientName).HasColumnName("recipient_name");
        builder.Property(x => x. RecipientPhone).HasColumnName("recipient_phone");
        builder.Property(x => x. Address).HasColumnName("address");
    }
}