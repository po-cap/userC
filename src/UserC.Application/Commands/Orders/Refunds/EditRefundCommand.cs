using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Entities.Orders;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Orders.Refunds;

/// <summary>
/// 買家設定退寬資訊
/// </summary>
public class EditRefundCommand : IRequest<Refund>
{
    /// <summary>
    /// 訂單編號
    /// </summary>
    public long OrderId { get; set; }

    #region 賣家設定

    /// <summary>
    /// 收件人名稱
    /// </summary>
    public string? ReceiptName { get; set; }

    /// <summary>
    /// 收件人電話號碼
    /// </summary>
    public string? ReceiptPhone { get; set; }

    /// <summary>
    /// 收件地址
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// 退款證明
    /// </summary>
    public string? ConfirmPayImage { get; set; }

    /// <summary>
    /// 確認收貨
    /// </summary>
    public bool? ConfirmPickup { get; set; }

    #endregion
    
    #region 買家設定

    /// <summary>
    /// 收款銀行名稱
    /// </summary>
    public string? BankName { get; set; }

    /// <summary>
    /// 收款銀行代碼
    /// </summary>
    public string? BankCode { get; set; }

    /// <summary>
    /// 收款銀帳戶
    /// </summary>
    public string? BankAccount { get; set; }

    /// <summary>
    /// 收款碼
    /// </summary>
    public string? ReceiveCodeImage { get; set; }

    /// <summary>
    /// 物流公司
    /// </summary>
    public string? ShippingProvider { get; set; }

    /// <summary>
    /// 物流單號
    /// </summary>
    public string? TrackingNumber { get; set; }
    
    /// <summary>
    /// 確認已收款
    /// </summary>
    public bool? ConfirmReceive { get; set; }

    #endregion
}

public class EditRefundHandler : IRequestHandler<EditRefundCommand, Refund>
{
    private readonly IAuthorizeUser _user;
    private readonly IOrderRepository _repository;

    public EditRefundHandler(
        IAuthorizeUser user, 
        IOrderRepository repository)
    {
        _user = user;
        _repository = repository;
    }
    
    public async Task<Refund> HandleAsync(EditRefundCommand request)
    {
        var order = await _repository.GetByIdAsync(
            request.OrderId,
            q => q.Include(x => x.Refund));
        if (order == null)
            throw Failure.NotFound();


        if (_user.Id == order.SellerId)
        {
            order.Refund.RecipientName = request.ReceiptName;
            order.Refund.RecipientPhone = request.ReceiptPhone;
            order.Refund.Address = request.Address;
            order.Refund.ConfirmPayImage = request.ConfirmPayImage;
            if (request.ConfirmPickup != null)
            {
                order.Refund.ConfirmPickup = request.ConfirmPickup.Value;
            }
        }
        else if (_user.Id == order.BuyerId)
        {
            order.Refund.BankName = request.BankName;
            order.Refund.BankCode = request.BankCode;
            order.Refund.BankAccount = request.BankAccount;
            order.Refund.ReceiveCodeImage = request.ReceiveCodeImage;
            order.Refund.ShippingProvider = request.ShippingProvider;
            order.Refund.TrackingNumber = request.TrackingNumber;
            if (request.ConfirmReceive != null)
            {
                order.Refund.ConfirmReceive = request.ConfirmReceive.Value;
            }
        }
        else
        {
            throw Failure.Unauthorized();
        }

        await _repository.SaveChangeAsync(order);

        return order.Refund;
    }
}