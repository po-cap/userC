using System.Text.Json;
using UserC.Domain.Entities;
using UserC.Domain.Entities.Items;

namespace UserC.Application.Models;

public class SkuModel
{
    /// <summary>
    /// ID
    /// </summary>
    public required long Id { get; set; }
    
    /// <summary>
    /// 名稱
    /// </summary>
    public required string name { get; set; }

    /// <summary>
    /// 規格（銷售屬性的規格）
    /// </summary>
    public required JsonDocument metadata { get; set; }
    
    /// <summary>
    /// 價錢
    /// </summary>
    public required double Price { get; set; }
        
    /// <summary>
    /// 數量
    /// </summary>
    public required int Quantity { get; set; }
}


public static partial class ModelConvertor
{
    public static SkuModel ToModel(this SKU sku)
    {
        return new SkuModel
        {
            Id = sku.Id,
            name = sku.Name,
            metadata = sku.Metadata ?? JsonDocument.Parse("{}"),
            Price = sku.Price,
            Quantity = sku.AvailableStock
        };
    }
} 