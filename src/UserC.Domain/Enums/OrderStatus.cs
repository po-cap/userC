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
    /// 評價中，
    /// 必須兩個人都評價後，訂單才算完成
    /// 這個狀態表示，還有一人沒有做評價
    /// </summary>
    reviewing = 5,
    
    /// <summary>
    /// 已取消
    /// </summary>
    cancelled = 6,
    
    /// <summary>
    /// 申請退款中
    /// </summary>
    refunding = 7,
    
    /// <summary>
    /// 交易完成
    /// </summary>
    completed = 8
}