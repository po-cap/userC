using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Orders;

/// <summary>
/// 買家確認收貨
/// </summary>
public class ConfirmReceiptCommand : IRequest
{
    /// <summary>
    /// 發起 command 的使用者的 ID
    /// </summary>
    public long UserId { get; set; }

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

    public ConfirmReceiptHandler(
        IOrderRepository repository, 
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }


    public async Task HandleAsync(ConfirmReceiptCommand request)
    {
        await _repository.MarkAsDeliveredAsync(
            id: request.OrderId,
            userId: request.UserId);

        await _unitOfWork.SaveChangeAsync();
    }
}