using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Enums;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Orders;

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
    private readonly IUnitOfWork _unitOfWork;

    public RefundHandler(
        IAuthorizeUser authorizeUser, 
        IOrderRepository repository, 
        IUnitOfWork unitOfWork)
    {
        _authorizeUser = authorizeUser;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task HandleAsync(RefundCommand request)
    {
        // 取得訂單
        var order  = await _repository.GetByIdAsync(request.OrderId);
        if (order == null)
            throw Failure.NotFound();
        
        // 查看權限，只有訂單的買家跟賣家可以執行
        var userId = _authorizeUser.Id;
        if (order.SellerId != userId && order.BuyerId != userId)
        {
            throw Failure.Unauthorized();
        }

        // 修改訂單狀態到 '退款中'
        order.Status = OrderStatus.refunding;

        // 完成 “資料庫 transaction“
        await _unitOfWork.SaveChangeAsync();
    }
}