using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Orders;

public class SetPaymentCommand : IRequest<bool>
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
    /// 匯款帳戶
    /// </summary>
    public string? BankAccount { get; set; }

    /// <summary>
    /// 收款碼
    /// </summary>
    public string? QrCodeImage { get; set; }
}

public class SetPaymentHandler : IRequestHandler<SetPaymentCommand, bool>
{
    private readonly IPaymentRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public SetPaymentHandler(
        IPaymentRepository repository, 
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }


    public async Task<bool> HandleAsync(SetPaymentCommand request)
    {
        try
        {
            await _repository.EditAccountAsync(
                orderId: request.OrderId,
                userId: request.UserId,
                bankAccount: request.BankAccount,
                qrCodeImage: request.QrCodeImage);

            await _unitOfWork.SaveChangeAsync();
            
            return true;
        }
        catch (Exception _)
        {
            return false;
        }
    }
}