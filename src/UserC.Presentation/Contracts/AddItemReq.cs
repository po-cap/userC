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
    public List<SkuDto> Skus { get; set; }
}

public static partial class ContractExtension
{
    public static AddItemCommand ToCommand(this AddItemReq request, string userId)
    {
        return new AddItemCommand()
        {
            UserId = userId,
            Description = request.Description,
            album = request.album,
            Skus = request.Skus
        };
    }
}

