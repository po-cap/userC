using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Enums;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Orders.Payments;

/// <summary>
/// 確認收款
/// </summary>
public class ReceivePayCommand : IRequest
{
    /// <summary>
    /// 訂單 ID
    /// </summary>
    public long OrderId { get; set; }
}

public class ReceivePayHandler : IRequestHandler<ReceivePayCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;
    private readonly IAuthorizeUser _authorizeUser;

    public ReceivePayHandler(
        IUnitOfWork unitOfWork, 
        IOrderRepository orderRepository, 
        IAuthorizeUser authorizeUser)
    {
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
        _authorizeUser = authorizeUser;
    }


    public async Task HandleAsync(ReceivePayCommand request)
    {
        //
        var order = await _orderRepository.GetByIdAsync(
            request.OrderId,
            q => q.Include(x => x.Record));
        if (order == null)
            throw Failure.NotFound();

        //
        var userId = _authorizeUser.Id;
        if (order.SellerId != userId)
            throw Failure.Unauthorized();
        
        //
        order.Record.PaidAt = DateTimeOffset.Now;
        order.Status = OrderStatus.paid;

        //
        await _unitOfWork.SaveChangeAsync();
    }
}