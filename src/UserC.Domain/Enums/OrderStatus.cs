namespace UserC.Domain.Enums;

public enum OrderStatus : short
{
    /// <summary>
    /// 待付款
    /// </summary>
    pending = 0,
    
    /// <summary>
    /// 已付款
    /// </summary>
    paid = 1,
    
    /// <summary>
    /// 處理中
    /// </summary>
    processing = 2,
    
    /// <summary>
    /// 已發貨
    /// </summary>
    shipped = 3,
    
    /// <summary>
    /// 已送達
    /// </summary>
    delivered = 4,
    
    /// <summary>
    /// 交易完成
    /// </summary>
    completed = 5,
    
    /// <summary>
    /// 申請退款中
    /// </summary>
    refunding = 6,
    
}