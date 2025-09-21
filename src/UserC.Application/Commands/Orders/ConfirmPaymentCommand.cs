using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Orders;

public class ConfirmPaymentCommand : IRequest<bool>
{
    /// <summary>
    /// 訂單 ID
    /// </summary>
    public long OrderId { get; set; }

    /// <summary>
    /// 變更者的 ID
    /// </summary>
    public long UserId { get; set; }
}

public class ConfirmPaymentHandler : IRequestHandler<ConfirmPaymentCommand, bool>
{
    private readonly IOrderRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmPaymentHandler(
        IOrderRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }


    public async Task<bool> HandleAsync(ConfirmPaymentCommand request)
    {
        try
        {
            // 設定為已付款
            await _repository.MarkAsPaid(request.OrderId);
            
            await _unitOfWork.SaveChangeAsync();
            
            return true;
        }
        catch (Exception _)
        {
            return false;
        }
    }
}