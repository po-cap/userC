namespace UserC.Domain.Enums;

public enum OrderStatus
{
    /// <summary>
    /// 待付款
    /// </summary>
    pending_payment = 1,
    
    /// <summary>
    /// 已付款
    /// </summary>
    paid = 2,
    
    /// <summary>
    /// 處理中
    /// </summary>
    processing = 3,
    
    /// <summary>
    /// 已發貨
    /// </summary>
    shipped = 4,
    
    /// <summary>
    /// 已送達
    /// </summary>
    delivered = 5,
    
    /// <summary>
    /// 已完成
    /// </summary>
    completed = 6,
    
    /// <summary>
    /// 已取消
    /// </summary>
    cancelled = 7,
    
    /// <summary>
    /// 已退款
    /// </summary>
    refunded = 8,
    
    /// <summary>
    /// 部分退款 
    /// </summary>
    partially_refunded = 9
}