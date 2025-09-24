using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Enums;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Orders;

/// <summary>
/// 買家確認收貨
/// </summary>
public class ConfirmReceiptCommand : IRequest
{
    /// <summary>
    /// 訂單編號
    /// </summary>
    public long OrderId { get; set; }
}

public class ConfirmReceiptHandler : IRequestHandler<ConfirmReceiptCommand>
{
    /// <summary>
    /// 倉儲
    /// </summary>
    private readonly IOrderRepository _repository;
    
    /// <summary>
    /// 操作單元
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// 認證用戶
    /// </summary>
    private readonly IAuthorizeUser _authorizeUser;

    public ConfirmReceiptHandler(
        IOrderRepository repository, 
        IUnitOfWork unitOfWork, 
        IAuthorizeUser authorizeUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _authorizeUser = authorizeUser;
    }


    public async Task HandleAsync(ConfirmReceiptCommand request)
    {
        var order = await _repository.GetByIdAsync(request.OrderId);
        if (order == null)
            throw Failure.NotFound();

        if (order.BuyerId != _authorizeUser.Id)
            throw Failure.Unauthorized();

        order.Status = OrderStatus.delivered;
        order.Record.DeliveredAt = DateTimeOffset.Now;
        
        await _unitOfWork.SaveChangeAsync();
    }
}