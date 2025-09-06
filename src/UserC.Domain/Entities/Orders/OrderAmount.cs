namespace UserC.Domain.Entities.Orders;

/// <summary>
/// 電單明細
/// </summary>
public class OrderAmount
{
    /// <summary>
    /// 訂單 ID
    /// </summary>
    public long OrderId { get; set; }
    
    /// <summary>
    /// 單價
    /// </summary>
    public double UnitPrice { get; set; }
    
    /// <summary>
    /// 數量
    /// </summary>
    public int Quantity { get; set; }
    
    /// <summary>
    /// 則扣價
    /// </summary>
    public double DiscountAmount { get; set; }
    
    /// <summary>
    /// 運費
    /// </summary>
    public double ShippingFee { get; set; }
    
    /// <summary>
    /// 總價
    /// </summary>
    public double TotalAmount { get; set; }
    
    /// <summary>
    /// 退款金額
    /// </summary>
    public double RefundAmount { get; set; }
    
    /// <summary>
    /// 退件數
    /// </summary>
    public int RefundQuantity  { get; set; }
}