using System.Text.Json;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Models;
using UserC.Application.Services;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Items;

public class EditItemCommand : IRequest
{
    /// <summary>
    /// 商品 ID
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// 商品描述
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// 運費
    /// </summary>
    public double ShippingFee { get; set; }
    
    /// <summary>
    /// 庫存單元
    /// </summary>
    public List<SkuModel> Skus { get; set; }
        
    /// <summary>
    /// 規格（關鍵數性，擴展屬性，或是價錢等等）
    /// </summary>
    public JsonDocument Extra { get; set; }
}


public class EditItemHandler : IRequestHandler<EditItemCommand>
{
    /// <summary>
    /// 認證用戶
    /// </summary>
    private readonly IAuthorizeUser _user;

    /// <summary>
    /// 倉儲
    /// </summary>
    private readonly IItemRepository _repository;

    public EditItemHandler(
        IAuthorizeUser user, 
        IItemRepository repository)
    {
        _user = user;
        _repository = repository;
    }

    public async Task HandleAsync(EditItemCommand request)
    {
        // 讀取商品資訊
        var item = await _repository.GetByIdAsync(request.Id);
        if(item == null)
            throw Failure.NotFound();

        // 變更商品
        item.Description = request.Description;
        item.ShippingFee = request.ShippingFee;
        item.Extra = request.Extra;

        //for (int i = 0; i < item.Skus.Count; i++)
        //{
        //    var model = request.Skus.First(x => x.Id == sku.Id);
        //}
        
        
        
        // 處存變更
        await _repository.SaveChangeAsync(item);
        
        throw new NotImplementedException();
    }
}