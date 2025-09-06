using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserC.Domain.Entities.Orders;
using UserC.Domain.Enums;

namespace UserC.Infrastructure.Persistence.Config;

public class OrderConfig : 
    IEntityTypeConfiguration<Order>,
    IEntityTypeConfiguration<OrderAmount>,
    IEntityTypeConfiguration<OrderRecord>,
    IEntityTypeConfiguration<OrderShipment>,
    IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        var statusConverter = new ValueConverter<OrderStatus, short>(
            v => (short)v,         // enum     -> smallint
            v => (OrderStatus)v  // smallint -> enum
        );
        
        builder.ToTable("orders").HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");
        
        builder.Property(x => x.BuyerId).HasColumnName("buyer_id");
        builder.Property(x => x.SellerId).HasColumnName("seller_id");
        builder.Property(x => x.ItemId).HasColumnName("item_id");
        builder.Property(x => x.Status)
               .HasColumnName("status")
               .HasConversion(statusConverter)
               .HasColumnType("smallint");
        builder.Property(x => x.Snapshot).HasColumnName("snapshot").HasColumnType("jsonb");

        builder.HasOne(x => x.Buyer).WithMany().HasForeignKey(x => x.BuyerId);
        builder.HasOne(x => x.Seller).WithMany().HasForeignKey(x => x.SellerId);
        builder.HasOne(x => x.Item).WithMany().HasForeignKey(x => x.ItemId);

        builder.HasOne(x => x.Amount).WithOne().HasForeignKey<OrderAmount>(x => x.OrderId);
        builder.HasOne(x => x.Record).WithOne().HasForeignKey<OrderRecord>(x => x.OrderId);
        builder.HasOne(x => x.Shipment).WithOne().HasForeignKey<OrderShipment>(x => x.OrderId);
    }

    public void Configure(EntityTypeBuilder<OrderAmount> builder)
    {
        builder.ToTable("orders_amount").HasKey(x => x.OrderId);

        builder.Property(x => x.OrderId).HasColumnName("order_id");
        builder.Property(x => x.UnitPrice).HasColumnName("unit_price");
        builder.Property(x => x.Quantity).HasColumnName("quantity");
        builder.Property(x => x.DiscountAmount).HasColumnName("discount_amount");
        builder.Property(x => x.ShippingFee).HasColumnName("shipping_fee");
        builder.Property(x => x.RefundAmount).HasColumnName("refund_amount");
        builder.Property(x => x.RefundQuantity).HasColumnName("refund_quantity");
    }

    public void Configure(EntityTypeBuilder<OrderRecord> builder)
    {
        builder.ToTable("orders_record").HasKey(x => x.OrderId);

        builder.Property(x => x.OrderId).HasColumnName("order_id");
        builder.Property(x => x.OrderAt).HasColumnName("order_at");
        builder.Property(x => x.PaidAt).HasColumnName("paid_at");
        builder.Property(x => x.ShippedAt).HasColumnName("shipped_at");
        builder.Property(x => x.DeliveredAt).HasColumnName("delivered_at");
        builder.Property(x => x.CompletedAt).HasColumnName("completed_at");
        builder.Property(x => x.CancelledAt).HasColumnName("cancelled_at");
        builder.Property(x => x.RefundAt).HasColumnName("refund_at");
    }

    public void Configure(EntityTypeBuilder<OrderShipment> builder)
    {
        builder.ToTable("orders_shipment").HasKey(x => x.OrderId);

        builder.Property(x => x.OrderId).HasColumnName("order_id");
        builder.Property(x => x.ShippingProvider).HasColumnName("shipping_provider");
        builder.Property(x => x.TrackingNumber).HasColumnName("tracking_number");
        builder.Property(x => x.RecipientName).HasColumnName("recipient_name");
        builder.Property(x => x.RecipientPhone).HasColumnName("recipient_phone");
        builder.Property(x => x.Address).HasColumnName("address");
        
    }

    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        var methodConverter = new ValueConverter<PaymentMethod, short>(
            v => (short)v,         // enum     -> smallint
            v => (PaymentMethod)v  // smallint -> enum
            );
        
        builder.ToTable("payment_records");

        builder.Property(x => x.BankAccount);
        builder.Property(x => x.QrCodeImage);
        builder.Property(x => x.ConfirmImage);
        builder.Property(x => x.Method).HasConversion(methodConverter).HasColumnName("smallint");
        builder.Property(x => x.PaidAt);
    }
}