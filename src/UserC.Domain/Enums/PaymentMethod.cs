namespace UserC.Domain.Enums;

public enum PaymentMethod : short
{
    /// <summary>
    /// 現金
    /// </summary>
    cash = 0,
    
    /// <summary>
    /// 台灣 Pay
    /// </summary>
    taiwan_pay = 1,
    
    /// <summary>
    /// 街口支付
    /// </summary>
    jko_pay = 2,
    
    /// <summary>
    /// 銀行轉帳
    /// </summary>
    bank_transfer = 3,
}