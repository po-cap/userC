namespace UserC.Domain.Entities;

public class Inventory
{
    /// <summary>
    /// 庫存單元 ID
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// 可用庫存
    /// </summary>
    public int AvailableStock { get; set; }

    /// <summary>
    /// 已分配庫存（如訂單佔用）
    /// </summary>
    public int AllocatedStock { get; set; }

    /// <summary>
    /// 預留庫存 (留給促銷活動用的等等)
    /// </summary>
    public int ReservedStock { get; set; }

    /// <summary>
    /// 低庫存門檻（低於這個的話，觸發補貨提醒）
    /// </summary>
    public int LowStackThreshold { get; set; }

    /// <summary>
    /// 更新時間
    /// </summary>
    public DateTimeOffset LastUpdated { get; set; }
}