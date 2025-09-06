using System.Text.Json;
using UserC.Domain.Entities.Items;

namespace UserC.Application.Models.Brief;

public class BriefItemModel
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
    /// 賣家名稱
    /// </summary>
    public required string DisplayName { get; set; }

    /// <summary>
    /// 賣家頭像
    /// </summary>
    public required string Avatar { get; set; }
}

public static partial class Convertor
{
    public static BriefItemModel ToBriefModel(this Item entity)
    {
        var metadata = entity.Extra.RootElement;
        
        return new BriefItemModel
        {
            Id          = entity.Id,
            SellerId    = entity.UserId,
            Cover       = metadata.GetProperty("cover").GetString()        ?? "",
            Description = entity.Description,
            Price       = metadata.GetProperty("price").GetString()        ?? "",
            DisplayName = metadata.GetProperty("displayName").GetString() ?? "",
            Avatar      = metadata.GetProperty("avatar").GetString()       ?? ""
        };
    }
} 