using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Enums;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Orders.Shipments;

/// <summary>
/// 簽收快遞
/// </summary>
public class PickupCommand : IRequest
{
    /// <summary>
    /// 訂單 ID
    /// </summary>
    public long OrderId { get; set; }
}

public class PickupHandler : IRequestHandler<PickupCommand>
{
    private readonly IAuthorizeUser _authorizeUser;
    private readonly IOrderRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public PickupHandler(
        IAuthorizeUser authorizeUser,
        IOrderRepository repository, 
        IUnitOfWork unitOfWork)
    {
        _authorizeUser = authorizeUser;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }


    public async Task HandleAsync(PickupCommand request)
    {
        //
        var order = await _repository.GetByIdAsync(
            request.OrderId,
            q => q.Include(x => x.Record));
        if(order == null)
            throw Failure.NotFound();

        //
        var userId = _authorizeUser.Id;
        if (order.BuyerId != userId)
            throw Failure.Unauthorized();

        //
        order.Status = OrderStatus.delivered;

        //
        var now = DateTime.Now;
        order.Record.ShippedAt ??= now;
        order.Record.PaidAt    ??= now;
        order.Record.DeliveredAt = now;

        //
        await _unitOfWork.SaveChangeAsync();
    }
}