using System.Text.Json;

namespace UserC.Domain.Entities;

public class SKU
{
    /// <summary>
    /// 庫存單元 ID
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// 標題
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// 相簿
    /// </summary>
    public string? Photo { get; set; }
    
    /// <summary>
    /// 規格
    /// </summary>
    public JsonDocument? Specs { get; set; }

    /// <summary>
    /// 價錢
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// 可用庫存
    /// </summary>
    public int AvailableStock { get; set; }

    /// <summary>
    /// 已分配庫存
    /// </summary>
    public int AllocatedStock { get; set; }

    /// <summary>
    /// Foreign Key - 鏈結 ID
    /// </summary>
    public long ItemId { get; set; }
    
    /// <summary>
    /// 倉庫
    /// </summary>
    public ICollection<Inventory> Inventories { get; set; }
}