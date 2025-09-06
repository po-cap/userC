using UserC.Domain.Entities.Items;

namespace UserC.Application.Models.Detailed;

public class DetailItemModel
{
    /// <summary>
    /// 商品 ID
    /// </summary>
    public required long Id { get; set; }

    /// <summary>
    /// 賣家 ID
    /// </summary>
    public required long SellerId { get; set; }
    
    /// <summary>
    /// 賣家名稱
    /// </summary>
    public required string DisplayName { get; set; }

    /// <summary>
    /// 賣家頭像
    /// </summary>
    public required string Avatar { get; set; }

    /// <summary>
    /// 封面照
    /// </summary>
    public required string Cover { get; set; }
    
    /// <summary>
    /// 描述
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// 價錢
    /// </summary>
    public required string Price { get; set; }

    /// <summary>
    /// 運費
    /// </summary>
    public required double ShippingFee { get; set; }

    /// <summary>
    /// 相側
    /// </summary>
    public required List<String> Assets { get; set; }

    /// <summary>
    /// 是否是視頻
    /// </summary>
    public required bool IsVideo { get; set; }

    /// <summary>
    /// 庫存單元
    /// </summary>
    public IEnumerable<SkuModel> Skus { get; set; }
}

public static partial class Convertor
{
    public static DetailItemModel ToDetailModel(this Item entity)
    {
        var metadata = entity.Extra.RootElement;
        return new DetailItemModel()
        {
            Id          = entity.Id,
            SellerId    = entity.UserId,
            Cover       = metadata.GetProperty("cover").GetString()        ?? "",
            Description = entity.Description,
            Price       = metadata.GetProperty("price").GetString()        ?? "",
            DisplayName = entity.Description,
            Avatar      = metadata.GetProperty("avatar").GetString()       ?? "",
            Assets       = entity.Album.Assets,
            IsVideo     = entity.Album.IsVideo,
            Skus        = from sku in entity.Skus select sku.ToModel(),
            ShippingFee = entity.ShippingFee
        };
    }
}