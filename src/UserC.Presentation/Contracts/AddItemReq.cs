using System.Text.Json;
using Po.Api.Response;
using UserC.Application.Commands.Items;
using UserC.Application.Models;

namespace UserC.Presentation.Contracts;

public class AddItemReq
{
    /// <summary>
    /// 商品描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 運費
    /// </summary>
    public double ShippingFee { get; set; }

    /// <summary>
    /// 相側
    /// </summary>
    public List<string> assets { get; set; }

    /// <summary>
    /// 庫存單元
    /// </summary>
    public List<SkuModel> Skus { get; set; }
    
    /// <summary>
    /// 規格（關鍵數性，擴展屬性，或是價錢等等）
    /// </summary>
    public JsonDocument Extra { get; set; }

    /// <summary>
    /// 是否是視頻
    /// </summary>
    public bool IsVideo { get; set; }
}

public static partial class ContractExtension
{
    public static AddItemCommand ToCommand(this AddItemReq request, long userId)
    {
        return new AddItemCommand()
        {
            UserId = userId,
            Description = request.Description,
            ShippingFee = request.ShippingFee,
            Assets = request.assets,
            Skus = request.Skus,
            Extra = request.Extra,
            IsVideo = request.IsVideo
        };
    }
}

