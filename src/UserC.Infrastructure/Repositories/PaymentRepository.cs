using Po.Api.Response;
using UserC.Domain.Enums;
using UserC.Domain.Repositories;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _dbContext;

    public PaymentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// 賣家設定付款資訊
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="userId"></param>
    /// <param name="bankAccount"></param>
    /// <param name="qrCodeImage"></param>
    public async Task EditAccountAsync(
        long orderId, 
        long userId, 
        string? bankAccount, 
        string? qrCodeImage)
    {
        var order = await _dbContext.Orders.FindAsync(orderId);
        if(order == null) throw Failure.NotFound();
        
        if(order.SellerId != userId) throw Failure.Unauthorized();
        
        var payment = await _dbContext.Payments.FindAsync(orderId);
        if(payment == null) throw Failure.NotFound();

        payment.BankAccount = bankAccount;
        payment.QrCodeImage = qrCodeImage;
    }

    /// <summary>
    /// 買家付款
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="userId"></param>
    /// <param name="confirmImage"></param>
    /// <param name="method"></param>
    public async Task PayAsync(
        long orderId, 
        long userId, 
        string? confirmImage, 
        PaymentMethod method)
    {
        var order = await _dbContext.Orders.FindAsync(orderId);
        if(order == null) throw Failure.NotFound();
        
        if(order.BuyerId != userId) throw Failure.Unauthorized();
        
        var payment = await _dbContext.Payments.FindAsync(orderId);
        if(payment == null) throw Failure.NotFound();

        payment.ConfirmImage = confirmImage;
        payment.Method = method;
    }

    /// <summary>
    /// 賣家確認傷到款項
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="userId"></param>
    public async Task ConfirmAsync(long orderId, long userId)
    {
        var order = await _dbContext.Orders.FindAsync(orderId);
        if(order == null) throw Failure.NotFound();
        
        if(order.SellerId != userId) throw Failure.Unauthorized();
        
        var payment = await _dbContext.Payments.FindAsync(orderId);
        if(payment == null) throw Failure.NotFound();

        payment.Confirm = true;
    }
}