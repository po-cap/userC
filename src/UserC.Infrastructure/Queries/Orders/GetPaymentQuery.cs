using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Domain.Entities.Orders;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Queries.Orders;

public class GetPaymentQuery : IRequest<Payment>
{
    /// <summary>
    /// 哪張 order
    /// </summary>
    public long OrderId { get; set; }

    /// <summary>
    /// 哪個使用者發起的請求
    /// </summary>
    public long UserId { get; set; }
}

public class GetPaymentHandler : IRequestHandler<GetPaymentQuery, Payment>
{
    private readonly AppDbContext _dbContext;

    public GetPaymentHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Payment> HandleAsync(GetPaymentQuery request)
    {
        var order = await _dbContext.Orders.FindAsync(request.OrderId);
        if (order == null)
            throw Failure.NotFound();

        if (order.SellerId != request.UserId && order.BuyerId != request.UserId)
            throw Failure.Unauthorized();
        
        var payment = await _dbContext.Payments.FindAsync(request.OrderId);
        if (payment == null)
        {
            payment = new Payment()
            {
                OrderId = request.OrderId
            };

            _dbContext.Payments.Add(payment);
            await _dbContext.SaveChangesAsync();
        }

        return payment;
    }
}