using System.Text.Json;
using UserC.Domain.Entities;

namespace UserC.Application.Models;

public class SkuDto
{
    /// <summary>
    /// 名稱
    /// </summary>
    public required string name { get; set; }

    /// <summary>
    /// 規格（銷售屬性的規格）
    /// </summary>
    public required JsonDocument Spec { get; set; }

    /// <summary>
    /// 照片
    /// </summary>
    public required string? photo { get; set; }
        
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
    public static SkuDto ToModel(this SKU sku)
    {
        return new SkuDto
        {
            name = sku.Name,
            Spec = sku.Specs ?? JsonDocument.Parse("{}"),
            photo = sku.Photo,
            Price = sku.Price,
            Quantity = sku.AvailableStock
        };
    }
} 