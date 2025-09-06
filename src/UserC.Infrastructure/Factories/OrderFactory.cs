using System.Text.Json;
using UserC.Application.Services;
using UserC.Domain.Entities;
using UserC.Domain.Entities.Orders;
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
    /// <param name="snapshot">快照</param>
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
        JsonDocument snapshot,
        string recipientName,
        string recipientPhone,
        string address
        )
    {
        return new Order()
        {
            Id       = _snowflake.Get(),
            SellerId = sellerId,
            BuyerId  = buyerId,
            ItemId = itemId,
            Status = OrderStatus.pending,
            Snapshot = snapshot,
            Amount = new OrderAmount()
            {
                UnitPrice = unitPrice,
                Quantity = quantity,
                DiscountAmount = discountAmount,
                ShippingFee = shippingFee,
                RefundAmount = 0,
                RefundQuantity = 0
            },
            Record = new OrderRecord()
            {
                OrderAt = DateTimeOffset.Now,
                PaidAt = null,
                ShippedAt = null,
                DeliveredAt = null,
                CompletedAt = null,
                CancelledAt = null,
                RefundAt = null
            },
            Shipment = new OrderShipment()
            {
                ShippingProvider = null,
                TrackingNumber = null,
                RecipientName = recipientName,
                RecipientPhone = recipientPhone,
                Address = address
            }
        };
    }
}