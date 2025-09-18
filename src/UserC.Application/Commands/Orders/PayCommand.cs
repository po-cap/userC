using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Enums;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Orders;

public class PayCommand : IRequest<bool>
{
    /// <summary>
    /// 訂單 ID
    /// </summary>
    public long OrderId { get; set; }

    /// <summary>
    /// 變更者的 ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 付款證明截圖
    /// </summary>
    public string? ConfirmImage { get; set; }

    /// <summary>
    /// 付款方式
    /// </summary>
    public PaymentMethod Method { get; set; }
}

public class PayHandler : IRequestHandler<PayCommand, bool>
{
    private readonly IPaymentRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public PayHandler(IPaymentRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> HandleAsync(PayCommand request)
    {
        try
        {
            await _repository.PayAsync(
                orderId: request.OrderId,
                userId: request.UserId,
                confirmImage: request.ConfirmImage,
                method: request.Method);

            await _unitOfWork.SaveChangeAsync();
            
            return true;
        }
        catch (Exception _)
        {
            return false;
        }
    }
}