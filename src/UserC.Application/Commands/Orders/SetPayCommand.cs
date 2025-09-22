using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Enums;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Orders;

public class SetPayoutDetailCommand : IRequest<bool>
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
    /// 銀行名稱
    /// </summary>
    public string? BankName { get; set; }
    
    /// <summary>
    /// 銀行代碼
    /// </summary>
    public string? BankCode { get; set; }

    /// <summary>
    /// 匯款帳戶
    /// </summary>
    public string? BankAccount { get; set; }

    /// <summary>
    /// 收款碼
    /// </summary>
    public string? QrCodeImage { get; set; }

    /// <summary>
    /// 付款方式
    /// </summary>
    public PaymentMethod Method { get; set; }
}

public class SetPayoutDetailHandler : IRequestHandler<SetPayoutDetailCommand, bool>
{
    private readonly IPaymentRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public SetPayoutDetailHandler(
        IPaymentRepository repository, 
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }


    public async Task<bool> HandleAsync(SetPayoutDetailCommand request)
    {
        try
        {
            await _repository.EditAccountAsync(
                orderId: request.OrderId,
                userId: request.UserId,
                bankName: request.BankName,
                bankCode: request.BankCode,
                bankAccount: request.BankAccount,
                qrCodeImage: request.QrCodeImage,
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