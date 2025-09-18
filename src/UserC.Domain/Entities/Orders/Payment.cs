using UserC.Domain.Enums;

namespace UserC.Domain.Entities.Orders;

public class Payment
{
    /// <summary>
    /// 交易 ID
    /// </summary>
    public long OrderId { get; set; }
    
    /// <summary>
    /// 收款銀行帳戶
    /// </summary>
    public string? BankAccount { get; set; }

    /// <summary>
    /// 收款 QrCode
    /// </summary>
    public string? QrCodeImage { get; set; }

    /// <summary>
    /// 確認付款照片
    /// </summary>
    public string? ConfirmImage { get; set; }

    /// <summary>
    /// 付款方式
    /// </summary>
    public PaymentMethod? Method { get; set; }

    /// <summary>
    /// 付款時間（也就是上傳確認付款 Image 的時間）
    /// </summary>
    public DateTimeOffset? PaidAt { get; set; }
    
    /// <summary>
    /// 賣家確認已收到款項
    /// </summary>
    public bool Confirm { get; set; }
}