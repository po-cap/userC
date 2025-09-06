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
    /// 已完成
    /// </summary>
    completed = 5,
    
    /// <summary>
    /// 已取消
    /// </summary>
    cancelled = 6,
    
    /// <summary>
    /// 已退款
    /// </summary>
    refunded = 7,
}