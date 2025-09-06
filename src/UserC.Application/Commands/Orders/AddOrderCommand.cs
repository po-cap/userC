using System.Text.Json;
using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Entities.Orders;
using UserC.Domain.Factories;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Orders;

public class AddOrderCommand : IRequest<Order>
{
    /// <summary>
    /// 賣家 ID
    /// </summary>
    public required long SellerId { get; set; }

    /// <summary>
    /// 買家 ID
    /// </summary>
    public required long BuyerId { get; set; }

    /// <summary>
    /// 單價
    /// </summary>
    public required double UnitPrice { get; set; }

    /// <summary>
    /// 數量
    /// </summary>
    public required int Quantity { get; set; }

    /// <summary>
    /// 折扣價
    /// </summary>
    public required double DiscountAmount { get; set; }

    /// <summary>
    /// 運費
    /// </summary>
    public required double ShippingFee { get; set; }

    /// <summary>
    /// 商品鏈結 ID
    /// </summary>
    public required long ItemId { get; set; }
    
    /// <summary>
    /// 快照
    /// </summary>
    public required JsonDocument Snapshot { get; set; }

    /// <summary>
    /// 收貨者名稱
    /// </summary>
    public required string RecipientName { get; set; }

    /// <summary>
    /// 收貨者電話
    /// </summary>
    public required string RecipientPhone { get; set; }

    /// <summary>
    /// 收貨地址
    /// </summary>
    public required string Address { get; set; }
}

internal class AddOrderHandler : IRequestHandler<AddOrderCommand, Order>
{
    private readonly IOrderFactory _orderFactory;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddOrderHandler(
        IOrderFactory orderFactory, 
        IOrderRepository orderRepository, 
        IUnitOfWork unitOfWork)
    {
        _orderFactory = orderFactory;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Order> HandleAsync(AddOrderCommand request)
    {
        var order = _orderFactory.New(
            sellerId:       request.SellerId,
            buyerId:        request.BuyerId,
            unitPrice:      request.UnitPrice,
            quantity:       request.Quantity,
            discountAmount: request.DiscountAmount,
            shippingFee:    request.ShippingFee,
            itemId:         request.ItemId,
            snapshot:       request.Snapshot,
            recipientName:  request.RecipientName,
            recipientPhone: request.RecipientPhone,
            address:        request.Address);
        
        _orderRepository.Add(order);

        await _unitOfWork.SaveChangeAsync();

        return order;
    }
}