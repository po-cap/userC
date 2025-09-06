using System.Text.Json;
using Shared.Mediator.Interface;
using UserC.Application.Models;
using UserC.Application.Services;
using UserC.Domain.Entities;
using UserC.Domain.Entities.Items;
using UserC.Domain.Factories;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Items;

public class AddItemCommand : IRequest<Item>
{
    /// <summary>
    /// 商品擁有者
    /// </summary>
    public long UserId { get; set; }
    
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
    public List<string> Assets { get; set; }

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

public class AddItemHandler : IRequestHandler<AddItemCommand, Item>
{
    private readonly IItemFactory _itemFactory;
    private readonly ISkuFactory _skuFactory;
    private readonly IItemRepository _itemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddItemHandler(
        IItemFactory itemFactory, 
        ISkuFactory skuFactory, 
        IItemRepository itemRepository,
        IUnitOfWork unitOfWork)
    {
        _itemFactory = itemFactory;
        _skuFactory = skuFactory;
        _itemRepository = itemRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Item> HandleAsync(AddItemCommand request)
    {
        // processing - 
        List<SKU> skus = [];
        foreach(var skuDto in request.Skus)
        {
            var sku = _skuFactory.Create(
                name: skuDto.name, 
                spec: skuDto.metadata, 
                price: skuDto.Price,
                quantity: skuDto.Quantity);
            skus.Add(sku);
        }

        // processing - 
        var item = _itemFactory.WithoutSPU(
            userId: request.UserId, 
            description: request.Description, 
            album: request.Assets,
            skus: skus,
            assets: request.Extra,
            shippingFee: request.ShippingFee,
            isVideo: request.IsVideo);
        
        // processing - 
        _itemRepository.Add(item);

        // processing - 
        await _unitOfWork.SaveChangeAsync();

        // return - 
        return item;
    }
}