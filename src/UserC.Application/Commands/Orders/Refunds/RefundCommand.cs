using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Orders.Refunds;

public class RefundCommand : IRequest
{
    /// <summary>
    /// 訂單 ID
    /// </summary>
    public long OrderId { get; set; }
}

public class RefundHandler : IRequestHandler<RefundCommand>
{
    private readonly IAuthorizeUser _authorizeUser;
    private readonly IOrderRepository _repository;

    public RefundHandler(
        IAuthorizeUser authorizeUser, 
        IOrderRepository repository)
    {
        _authorizeUser = authorizeUser;
        _repository = repository;
    }
    
    public async Task HandleAsync(RefundCommand request)
    {
        // 取得訂單
        var order  = await _repository.GetByIdAsync(request.OrderId);
        if (order == null)
            throw Failure.NotFound();
        
        //
        order.OnRefund(_authorizeUser.Id);

        //
        await _repository.SaveChangeAsync(order);
    }
}