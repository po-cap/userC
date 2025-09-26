using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Orders.Payments;

public class PayCommand : IRequest
{
    /// <summary>
    /// 訂單 ID
    /// </summary>
    public long OrderId { get; set; }
    
    /// <summary>
    /// 付款證明截圖
    /// </summary>
    public string? ConfirmImage { get; set; }
}

public class PayHandler : IRequestHandler<PayCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;
    private readonly IAuthorizeUser _authorizeUser;
    
    public PayHandler(
        IUnitOfWork unitOfWork, 
        IOrderRepository orderRepository, 
        IAuthorizeUser authorizeUser)
    {
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
        _authorizeUser = authorizeUser;
    }

    public async Task HandleAsync(PayCommand request)
    {
        //
        var order = await _orderRepository.GetByIdAsync(
            request.OrderId,
            q => q.Include(x => x.Payment));
        if (order == null)
            throw Failure.NotFound();

        //
        var userId = _authorizeUser.Id;
        if (order.BuyerId != userId)
            throw Failure.Unauthorized();

        //
        order.Payment.ConfirmImage = request.ConfirmImage;
        order.Payment.PaidAt = DateTimeOffset.Now;
        
        //
        await _unitOfWork.SaveChangeAsync();
    }
}