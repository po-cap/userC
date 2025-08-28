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
    /// 相側
    /// </summary>
    public List<string> album { get; set; }

    /// <summary>
    /// 庫存單元
    /// </summary>
    public List<SkuModel> Skus { get; set; }
    
    /// <summary>
    /// 規格（關鍵數性，擴展屬性，或是價錢等等）
    /// </summary>
    public JsonDocument Spec { get; set; }
}

public static partial class ContractExtension
{
    public static AddItemCommand ToCommand(this AddItemReq request, string sub)
    {
        if(!long.TryParse(sub, out var id)) 
            throw Failure.Unauthorized();
        
        return new AddItemCommand()
        {
            UserId = id,
            Description = request.Description,
            album = request.album,
            Skus = request.Skus,
            Spec = request.Spec
        };
    }
}

