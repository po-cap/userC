namespace UserC.Domain.Entities;

public enum Status
{
    /// <summary>
    /// 待審核
    /// </summary>
    pending = 1,
    
    /// <summary>
    /// 啟用
    /// </summary>
    active = 2,
    
    /// <summary>
    /// 暫停
    /// </summary>
    suspended = 3,
    
    /// <summary>
    /// 關閉
    /// </summary>
    closed = 4
}