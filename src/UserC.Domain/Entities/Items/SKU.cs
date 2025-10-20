using System.Text.Json;

namespace UserC.Domain.Entities.Items;

public class SKU
{
    public static SKU New(
        long id,
        string name, 
        JsonDocument spec,
        double price, 
        int quantity)
    {
        return new SKU
        {
            Id = id,
            Name = name,
            Metadata = spec,
            Price = price,
            AvailableStock = quantity,
            AllocatedStock = 0,
        };
    }
    
    /// <summary>
    /// 庫存單元 ID
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// 標題
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// 規格
    /// </summary>
    public JsonDocument Metadata { get; set; }

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
}