using System.Text.Json;
using UserC.Application.Services;
using UserC.Domain.Entities;
using UserC.Domain.Enums;
using UserC.Domain.Factories;

namespace UserC.Infrastructure.Factories;

public class OrderFactory : IOrderFactory
{
    private readonly Snowflake _snowflake;

    public OrderFactory(Snowflake snowflake)
    {
        _snowflake = snowflake;
    }
    
    /// <summary>
    /// 建立訂單
    /// </summary>
    /// <param name="sellerId">賣家 ID</param>
    /// <param name="buyerId">買家 ID</param>
    /// <param name="unitPrice">單價</param>
    /// <param name="quantity">數量</param>
    /// <param name="discountAmount">折扣價</param>
    /// <param name="shippingFee">運費</param>
    /// <param name="itemId">商品鏈結 ID</param>
    /// <param name="skuId">庫存單元 ID</param>
    /// <param name="itemSpec">商品快照</param>
    /// <param name="skuSpec">庫存單元快照</param>
    /// <param name="recipientName">收貨者名稱</param>
    /// <param name="recipientPhone">收貨者電話</param>
    /// <param name="address">收貨地址</param>
    /// <returns></returns>
    public Order New(
        long sellerId, 
        long buyerId,
        double unitPrice,
        int quantity,
        double discountAmount,
        double shippingFee,
        long itemId,
        long skuId,
        JsonDocument itemSpec,
        JsonDocument skuSpec,
        string recipientName,
        string recipientPhone,
        string address
        )
    {
        return new Order()
        {
            // 基本訊息
            Id       = _snowflake.Get(),
            SellerId = sellerId,
            BuyerId  = buyerId,
            
            // 金額訊息
            OrderAmount    = unitPrice * quantity,
            DiscountAmount = discountAmount,
            TaxAmount      = 0,
            ShippingFee    = shippingFee,
            TotalAmount    = unitPrice * quantity - discountAmount - shippingFee,
            
            // 物流訊息
            ShippingProvider = null,
            TrackingNumber   = null,
            
            // 訂單狀態
            Status      = OrderStatus.pending_payment,
            OrderAt     = DateTimeOffset.Now,
            PaidAt      = null,
            ShippedAt   = null,
            DeliveredAt = null,
            CompletedAt = null,
            CancelledAt = null,
            RefundAt    = null,
            
            // 商品訊息
            ItemId    = itemId,
            SkuId     = skuId,
            ItemSpec  = itemSpec,
            SkuSpec   = skuSpec,
            UnitPrice = unitPrice,
            Quantity  = quantity,
            
            // 退貨訊息
            RefundAmount   = 0,
            RefundQuantity = 0,
            
            // 收貨訊息
            RecipientName  = recipientName,
            RecipientPhone = recipientPhone,
            Address        = address
            
        };
    }
}