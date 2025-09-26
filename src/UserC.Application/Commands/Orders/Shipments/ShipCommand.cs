using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Enums;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Orders.Shipments;

/// <summary>
/// 發貨
/// </summary>
public class ShipCommand : IRequest
{
    /// <summary>
    /// 訂單編號
    /// </summary>
    public required long orderId { get; set; }

    /// <summary>
    /// 物流公司
    /// </summary>
    public required string ShippingProvider { get; set; }

    /// <summary>
    /// 物流單號
    /// </summary>
    public required string TrackingNumber { get; set; }
}

public class ShipHandler : IRequestHandler<ShipCommand>
{
    private readonly IOrderRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorizeUser _authorizeUser;

    public ShipHandler(
        IOrderRepository repository, 
        IUnitOfWork unitOfWork, 
        IAuthorizeUser authorizeUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _authorizeUser = authorizeUser;
    }


    public async Task HandleAsync(ShipCommand request)
    {
        var userId = _authorizeUser.Id;

        var order = await _repository.GetByIdAsync(
            request.orderId,
            q => q.Include(x => x.Record).Include(x => x.Shipment));
        if (order == null)
            throw Failure.NotFound();

        if (order.SellerId != userId)
            throw Failure.Unauthorized();

        order.Status = OrderStatus.shipped;
        order.Record.ShippedAt = DateTimeOffset.Now;
        order.Shipment.ShippingProvider = request.ShippingProvider;
        order.Shipment.TrackingNumber = request.TrackingNumber;

        await _unitOfWork.SaveChangeAsync();
    }
}

