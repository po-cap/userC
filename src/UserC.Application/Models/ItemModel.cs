using System.Text.Json;
using UserC.Domain.Entities;

namespace UserC.Application.Models;

public class ItemModel
{
    /// <summary>
    /// 鏈結 ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 使用者
    /// </summary>
    public UserModel User { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 運費
    /// </summary>
    public double ShippingFee { get; set; }

    /// <summary>
    /// 相簿
    /// </summary>
    public List<string> Album { get; set; }

    /// <summary>
    /// 規格
    /// </summary>
    public JsonDocument Spec { get; set; }
}

public static partial class ItemExtension 
{
    public static ItemModel ToModel(this Item item)
    {
        return new ItemModel()
        {
            Id = item.Id,
            User = item.User.ToModel(),
            Description = item.Description,
            ShippingFee = item.ShippingFee,
            Album = item.Albums,
            Spec = item.Specs ?? JsonDocument.Parse("{}")
        };
    }
}