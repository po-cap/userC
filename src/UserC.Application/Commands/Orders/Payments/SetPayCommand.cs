using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Enums;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Orders.Payments;

/// <summary>
/// 設定收款資訊
/// </summary>
public class SetPayCommand : IRequest
{
    /// <summary>
    /// 訂單 ID
    /// </summary>
    public long OrderId { get; set; }
    
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

public class SetPayHandler : IRequestHandler<SetPayCommand>
{
    private readonly IOrderRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorizeUser _authorizeUser;
    
    public SetPayHandler(
        IOrderRepository repository,
        IUnitOfWork unitOfWork, 
        IAuthorizeUser authorizeUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _authorizeUser = authorizeUser;
    }


    public async Task HandleAsync(SetPayCommand request)
    {
        //
        var order = await _repository.GetByIdAsync(
            request.OrderId,
            q => q.Include(x => x.Payment));
        if (order == null)
            throw Failure.NotFound();

        //
        var userId = _authorizeUser.Id;
        if (order.SellerId != userId)
            throw Failure.Unauthorized();

        //
        if (request.QrCodeImage == null)
        {
            if (request.BankName == null ||
                request.BankCode == null ||
                request.BankAccount == null)
            {
                throw Failure.BadRequest("請填寫完整訊息");
            }

            order.Payment.BankName = request.BankName;
            order.Payment.BankCode = request.BankCode;
            order.Payment.BankAccount = request.BankAccount;
            order.Payment.Method = PaymentMethod.bank_transfer;
        }
        //
        else
        {
            order.Payment.QrCodeImage = request.QrCodeImage;
            order.Payment.Method = PaymentMethod.qr_code;
        }

        //
        await _unitOfWork.SaveChangeAsync();
    }
}