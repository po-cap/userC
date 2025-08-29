using System.Text.Json;
using UserC.Domain.Entities;

namespace UserC.Application.Models;

public class OrderModel
{
    /// <summary>
    /// 訂單編號
    /// </summary>
    public required long Id { get; set; }
    
    #region 時間線
    
    /// <summary>
    /// 下單時間
    /// </summary>
    public required DateTimeOffset OrderAt { get; set; }
    
    /// <summary>
    /// 付款時間
    /// </summary>
    public required DateTimeOffset? PaidAt { get; set; }
    
    /// <summary>
    /// 發貨時間
    /// </summary>
    public required DateTimeOffset? ShippedAt { get; set; }

    /// <summary>
    /// 完成訂單時間
    /// </summary>
    public required DateTimeOffset? CompleteAt { get; set; }
    
    #endregion

    #region 物流資訊

    /// <summary>
    /// 物流方式
    /// </summary>
    public required string? ShippingProvider { get; set; }

    /// <summary>
    /// Tracking Number
    /// </summary>
    public required string? TrackingNumber { get; set; }

    /// <summary>
    /// 收貨者名稱
    /// </summary>
    public required string RecipientName { get; set; }
    
    /// <summary>
    /// 收貨者號碼
    /// </summary>
    public required string RecipientPhone { get; set; }
    
    /// <summary>
    /// 收貨地址
    /// </summary>
    public required string Address { get; set; }
    
    #endregion

    #region 金額

    /// <summary>
    /// 訂單金額
    /// </summary>
    public required double OrderAmount { get; set; }

    /// <summary>
    /// 則扣
    /// </summary>
    public required double DiscountAmount { get; set; }

    /// <summary>
    /// 運費
    /// </summary>
    public required double ShippingFee { get; set; }

    /// <summary>
    /// 總價
    /// </summary>
    public required double TotalAmount { get; set; }

    #endregion

    #region 商品資訊

    /// <summary>
    /// 商品鏈結快照
    /// </summary>
    public required JsonDocument Item { get; set; }

    /// <summary>
    /// 單位庫存快照
    /// </summary>
    public required JsonDocument Sku { get; set; }

    /// <summary>
    /// 數量
    /// </summary>
    public required int Quantity { get; set; }

    #endregion
}


public static partial class ModelConvertor
{
    public static OrderModel ToModel(this Order entity)
    {
        return new OrderModel
        {
            Id               = entity.Id,
            OrderAt          = entity.OrderAt,
            PaidAt           = entity.PaidAt,
            ShippedAt        = entity.ShippedAt,
            CompleteAt       = entity.CompletedAt,
            ShippingProvider = entity.ShippingProvider,
            TrackingNumber   = entity.TrackingNumber,
            RecipientName    = entity.RecipientName,
            RecipientPhone   = entity.RecipientPhone,
            Address          = entity.Address,
            OrderAmount      = entity.OrderAmount,
            DiscountAmount   = entity.DiscountAmount,
            ShippingFee      = entity.ShippingFee,
            TotalAmount      = entity.TotalAmount,
            Item             = entity.ItemSpec,
            Sku              = entity.SkuSpec,
            Quantity         = entity.Quantity
        };
    }
}