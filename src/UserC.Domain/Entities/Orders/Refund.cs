namespace UserC.Domain.Entities.Orders;

public class Refund
{
    /// <summary>
    /// 訂單 ID
    /// </summary>
    public long OrderId { get; set; }

    /// <summary>
    /// 收款銀行名稱
    /// </summary>
    public string? BankName { get; set; }
    
    /// <summary>
    /// 收款銀代碼
    /// </summary>
    public string? BankCode { get; set; }
    
    /// <summary>
    /// 收款銀行帳戶
    /// </summary>
    public string? BankAccount { get; set; }
    
    /// <summary>
    /// 收款碼
    /// </summary>
    public string? ReceiveCodeImage { get; set; }
    
    /// <summary>
    /// 退款證明
    /// </summary>
    public string? ConfirmPayImage { get; set; }

    /// <summary>
    /// 退貨收件人姓名
    /// </summary>
    public string? RecipientName { get; set; }
    
    /// <summary>
    /// 退貨收件人電話
    /// </summary>
    public string? RecipientPhone { get; set; }
    
    /// <summary>
    /// 退貨地址
    /// </summary>
    public string? Address { get; set; }
    
    /// <summary>
    /// 物流公司
    /// </summary>
    public string? ShippingProvider { get; set; }
    
    /// <summary>
    /// 物流單號
    /// </summary>
    public string? TrackingNumber { get; set; }
    
    /// <summary>
    /// 確認收到退貨
    /// </summary>
    public bool ConfirmPickup { get; set; }
    
    /// <summary>
    /// 確認收到退款
    /// </summary>
    public bool ConfirmReceive { get; set; }
}