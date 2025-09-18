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
    /// <param name="bankAccount"></param>
    /// <param name="qrCodeImage"></param>
    Task EditAccountAsync(
        long orderId, 
        long userId, 
        string? bankAccount, 
        string? qrCodeImage);

    /// <summary>
    /// 買家付款
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="userId"></param>
    /// <param name="confirmImage"></param>
    /// <param name="method"></param>
    Task PayAsync(
        long orderId, 
        long userId, 
        string? confirmImage, 
        PaymentMethod method);
    
    /// <summary>
    /// 賣家確認傷到款項
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="userId"></param>
    Task ConfirmAsync(long orderId, long userId);
}