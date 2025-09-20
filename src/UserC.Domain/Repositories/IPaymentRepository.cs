using UserC.Domain.Entities.Orders;
using UserC.Domain.Enums;

namespace UserC.Domain.Repositories;

public interface IPaymentRepository
{
    /// <summary>
    /// 賣家設定付款資訊
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="userId"></param>
    /// <param name="bankName"></param>
    /// <param name="bankCode"></param> 
    /// <param name="bankAccount"></param>
    /// <param name="qrCodeImage"></param>
    /// <param name="method"></param>
    Task EditAccountAsync(
        long orderId, 
        long userId, 
        string? bankName,
        string? bankCode,
        string? bankAccount, 
        string? qrCodeImage,
        PaymentMethod method);

    /// <summary>
    /// 買家付款
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="userId"></param>
    /// <param name="confirmImage"></param>
    Task PayAsync(
        long orderId, 
        long userId, 
        string? confirmImage);
    
    /// <summary>
    /// 賣家確認傷到款項
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="userId"></param>
    Task ConfirmAsync(long orderId, long userId);
}