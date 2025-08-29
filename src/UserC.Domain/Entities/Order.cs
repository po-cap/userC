using System.Text.Json;
using UserC.Domain.Enums;

namespace UserC.Domain.Entities;

public class Order
{
    #region 基本資訊
    
    /// <summary>
    /// ID
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// 買家 ID
    /// </summary>
    public long BuyerId { get; set; }
    
    /// <summary>
    /// 賣家 ID
    /// </summary>
    public long SellerId { get; set; }
    
    #endregion
    
    #region 金額資訊
    
    /// <summary>
    /// 物品總價
    /// </summary>
    public double OrderAmount { get; set; }
    
    /// <summary>
    /// 則扣價
    /// </summary>
    public double DiscountAmount { get; set; }
    
    /// <summary>
    /// 稅價
    /// </summary>
    public double TaxAmount { get; set; }
    
    /// <summary>
    /// 運費
    /// </summary>
    public double ShippingFee { get; set; }
    
    /// <summary>
    /// 總價
    /// </summary>
    public double TotalAmount { get; set; }
    
    #endregion

    
    
    #region 貨運資訊
    
    /// <summary>
    /// 貨運公司(seven, family, post)
    /// </summary>
    public string? ShippingProvider { get; set; }
    
    /// <summary>
    /// Tracking Number
    /// </summary>
    public string? TrackingNumber { get; set; }
    
    #endregion

    
    #region 備註資訊
    
    /// <summary>
    /// 買家備注
    /// </summary>
    public string BuyerNote { get; set; }
    
    /// <summary>
    /// 賣家備注
    /// </summary>
    public string SellerNote { get; set; }
    
    #endregion

    
    
    #region 狀態資訊
    
    /// <summary>
    /// 狀態
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// 下單時間
    /// </summary>
    public DateTimeOffset OrderAt { get; set; }
    
    /// <summary>
    /// 付款時間
    /// </summary>
    public DateTimeOffset? PaidAt { get; set; }
    
    /// <summary>
    /// 物流取貨時間
    /// </summary>
    public DateTimeOffset? ShippedAt { get; set; }
    
    /// <summary>
    /// 物流送達時間
    /// </summary>
    public DateTimeOffset? DeliveredAt { get; set; }  
    
    /// <summary>
    /// 訂單完成時間
    /// </summary>
    public DateTimeOffset? CompletedAt { get; set; }
    
    /// <summary>
    /// 訂單取消時間
    /// </summary>
    public DateTimeOffset? CancelledAt { get; set; }
    
    /// <summary>
    /// 訂單退款時間
    /// </summary>
    public DateTimeOffset? RefundAt { get; set; }
    
    #endregion


   
    #region 商品資訊
    
    /// <summary>
    /// Item ID
    /// </summary>
    public long ItemId { get; set; }
    
    /// <summary>
    /// SKU ID
    /// </summary>
    public long SkuId { get; set; }
    
    /// <summary>
    /// Item 快照
    /// </summary>
    public JsonDocument ItemSpec { get; set; }
    
    /// <summary>
    /// SKU 快照
    /// </summary>
    public JsonDocument SkuSpec { get; set; }

    /// <summary>
    /// 單價
    /// </summary>
    public double UnitPrice { get; set; }
    
    /// <summary>
    /// 數量
    /// </summary>
    public int Quantity { get; set; }
    
    #endregion


    
    #region 退款資訊
    
    /// <summary>
    /// 退款金額
    /// </summary>
    public double RefundAmount { get; set; }
    
    /// <summary>
    /// 退件數
    /// </summary>
    public int RefundQuantity  { get; set; }
    
    #endregion

    
    #region 寄件地址資訊
    
    /// <summary>
    /// 收件者名稱
    /// </summary>
    public string RecipientName { get; set; }
    
    /// <summary>
    /// 收件者電話
    /// </summary>
    public string RecipientPhone { get; set; }
    
    /// <summary>
    /// 收件地址
    /// </summary>
    public string Address { get; set; }
    
    #endregion

}