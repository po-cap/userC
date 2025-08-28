using System.Text.Json;

namespace UserC.Domain.Entities;

public class Item
{
    /// <summary>
    /// 連接 ID
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// 商品 ID
    /// </summary>
    public long? SpuId { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 類目 1 - ID
    /// </summary>
    public long? Category1Id { get; set; }

    /// <summary>
    /// 類目 2 - ID
    /// </summary>
    public long? Category2Id { get; set; }
    
    /// <summary>
    /// 類目 3 - ID
    /// </summary>
    public long? Category3Id { get; set; }

    /// <summary>
    /// 品牌 - ID
    /// </summary>
    public long? BrandId { get; set; }

    /// <summary>
    /// 是否為服務
    /// </summary>
    public bool IsService { get; set; }

    /// <summary>
    /// 相簿
    /// </summary>
    public required List<string> Albums { get; set; }

    /// <summary>
    /// 運費
    /// </summary>
    public double ShippingFee { get; set; }
    
    /// <summary>
    /// 規格
    /// </summary>
    public JsonDocument? Specs { get; set; }

    /// <summary>
    /// 庫存
    /// </summary>
    public ICollection<SKU> Skus { get; set; }

    /// <summary>
    /// 這個鏈結是哪個用戶發的
    /// </summary>
    public required long UserId { get; set; }
    
    /// <summary>
    /// 使用者
    /// </summary>
    public User User { get; set; }
}