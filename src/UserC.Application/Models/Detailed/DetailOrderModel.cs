using UserC.Domain.Entities.Orders;

namespace UserC.Application.Models.Detailed;

public class DetailOrderModel
{
    /// <summary>
    /// 交易 ID
    /// </summary>
    public required long Id { get; set; }
    
    
    
    /// <summary>
    /// 對方的名稱
    /// </summary>
    public required string DisplayName { get; set; }

    /// <summary>
    /// 商品照片
    /// </summary>
    public required string Cover { get; set; }

    /// <summary>
    /// 商品簡介
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// 庫存單元名稱
    /// </summary>
    public required string SkuName { get; set; }
    
    /// <summary>
    /// 單價
    /// </summary>
    public required double UnitPrice { get; set; }
    
    /// <summary>
    /// 數量
    /// </summary>
    public required int Quantity { get; set; }
    
    /// <summary>
    /// 則扣價
    /// </summary>
    public required double DiscountAmount { get; set; }
    
    /// <summary>
    /// 運費
    /// </summary>
    public required double ShippingFee { get; set; }
    
    /// <summary>
    /// 交易價格
    /// </summary>
    public required double TotalAmount { get; set; }
    
    
    
    /// <summary>
    /// 下單時間
    /// </summary>
    public required DateTimeOffset OrderAt { get; set; }
    
    /// <summary>
    /// 付款時間
    /// </summary>
    public required DateTimeOffset? PaidAt { get; set; }
    
    /// <summary>
    /// 物流取貨時間
    /// </summary>
    public required DateTimeOffset? ShippedAt { get; set; }
    
    /// <summary>
    /// 物流送達時間
    /// </summary>
    public required DateTimeOffset? DeliveredAt { get; set; }  
    
    /// <summary>
    /// 訂單取消時間
    /// </summary>
    public required DateTimeOffset? CancelledAt { get; set; }
    
    /// <summary>
    /// 訂單完成時間
    /// </summary>
    public required DateTimeOffset? CompletedAt { get; set; }
    
    /// <summary>
    /// 訂單退款時間
    /// </summary>
    public required DateTimeOffset? RefundAt { get; set; }
    
    
    
    /// <summary>
    /// 貨運公司(seven, family, post)
    /// </summary>
    public required string? ShippingProvider { get; set; }
    
    /// <summary>
    /// Tracking Number
    /// </summary>
    public required string? TrackingNumber { get; set; }
    
    /// <summary>
    /// 收件者名稱
    /// </summary>
    public required string RecipientName { get; set; }
    
    /// <summary>
    /// 收件者電話
    /// </summary>
    public required string RecipientPhone { get; set; }
    
    /// <summary>
    /// 收件地址
    /// </summary>
    public required string Address { get; set; }
}

public static partial class Convertor
{
    public static DetailOrderModel ToDetailModel(this Order entity, bool isBuyer)
    {
        var metadata = entity.Snapshot.RootElement;

        return new DetailOrderModel
        {
            Id          = entity.Id,
            DisplayName = isBuyer ? entity.Seller.DisplayName : entity.Buyer.DisplayName,
            Cover       = metadata.GetProperty("display_name").GetString() ?? "",
            Description = metadata.GetProperty("description").GetString() ?? "",
            SkuName     = metadata.GetProperty("sku_name").GetString() ?? "",
            
            UnitPrice      = entity.Amount.UnitPrice,
            Quantity       = entity.Amount.Quantity,
            DiscountAmount = entity.Amount.DiscountAmount,
            ShippingFee    = entity.Amount.ShippingFee,
            TotalAmount    = entity.Amount.TotalAmount,
            
            OrderAt     = entity.Record.OrderAt,
            PaidAt      = entity.Record.PaidAt,
            ShippedAt   = entity.Record.ShippedAt,
            DeliveredAt = entity.Record.DeliveredAt,
            CancelledAt = entity.Record.CancelledAt,
            CompletedAt = entity.Record.CompletedAt,
            RefundAt    = entity.Record.RefundAt,
            
            ShippingProvider = entity.Shipment.ShippingProvider,
            TrackingNumber   = entity.Shipment.TrackingNumber,
            RecipientName    = entity.Shipment.RecipientName,
            RecipientPhone   = entity.Shipment.RecipientPhone,
            Address          = entity.Shipment.Address,
        };
    }
}