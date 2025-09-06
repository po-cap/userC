namespace UserC.Domain.Entities.Orders;

/// <summary>
/// 訂單記事
/// </summary>
public class OrderRecord
{
    /// <summary>
    /// 訂單 ID
    /// </summary>
    public long OrderId { get; set; }
    
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
}