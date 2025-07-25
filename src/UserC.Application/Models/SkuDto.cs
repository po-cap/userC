using System.Text.Json;

namespace UserC.Application.Models;

public class SkuDto
{
    /// <summary>
    /// 名稱
    /// </summary>
    public string name { get; set; }

    /// <summary>
    /// 規格（銷售屬性的規格）
    /// </summary>
    public JsonDocument Spec { get; set; }

    /// <summary>
    /// 照片
    /// </summary>
    public string? photo { get; set; }
        
    /// <summary>
    /// 價錢
    /// </summary>
    public double Price { get; set; }
        
    /// <summary>
    /// 數量
    /// </summary>
    public int Quantity { get; set; }
}