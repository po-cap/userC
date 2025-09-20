namespace UserC.Domain.Enums;

public enum PaymentMethod : short
{
    /// <summary>
    /// 現金
    /// </summary>
    cash = 0,
    
    /// <summary>
    /// 付款碼
    /// </summary>
    qr_code = 1,
    
    /// <summary>
    /// 銀行轉帳
    /// </summary>
    bank_transfer = 2,
}