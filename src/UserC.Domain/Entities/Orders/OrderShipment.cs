namespace UserC.Domain.Entities.Orders;

/// <summary>
/// 訂單運貨資訊
/// </summary>
public class OrderShipment
{
    /// <summary>
    /// 訂單 ID
    /// </summary>
    public long OrderId { get; set; }
    
    /// <summary>
    /// 貨運公司(seven, family, post)
    /// </summary>
    public string? ShippingProvider { get; set; }
    
    /// <summary>
    /// Tracking Number
    /// </summary>
    public string? TrackingNumber { get; set; }
    
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
}