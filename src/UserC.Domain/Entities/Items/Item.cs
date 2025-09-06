using System.Text.Json;

namespace UserC.Domain.Entities.Items;

public class Item
{
    /// <summary>
    /// 連接 ID
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// 這個鏈結是哪個用戶發的
    /// </summary>
    public required long UserId { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 類目 1 - ID
    /// </summary>
    public long? CategoryId { get; set; }
    
    /// <summary>
    /// 運費
    /// </summary>
    public double ShippingFee { get; set; }
    
    /// <summary>
    /// 規格
    /// </summary>
    public JsonDocument Extra { get; set; }

    /// <summary>
    /// 是否下架
    /// </summary>
    public bool DeListed { get; set; }
    
    /// <summary>
    /// foreign key - 使用者
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// foreign key - 相簿
    /// </summary>
    public Album Album { get; set; }

    /// <summary>
    /// foreign key - 庫存單元
    /// </summary>
    public List<SKU> Skus { get; set; }
}

