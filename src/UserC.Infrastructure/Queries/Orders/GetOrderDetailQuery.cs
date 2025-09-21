using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Models.Detailed;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Queries.Orders;

public class GetOrderDetailQuery : IRequest<DetailOrderModel>
{
    /// <summary>
    /// 使用者 ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 是用者是否是買家
    /// </summary>
    public bool IsBuyer { get; set; }

    /// <summary>
    /// 訂單編號
    /// </summary>
    public long OrderId { get; set; }
}

public class GetOrderDetailHandler : IRequestHandler<GetOrderDetailQuery, DetailOrderModel>
{
    private readonly AppDbContext _dbContext;

    public GetOrderDetailHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async  Task<DetailOrderModel> HandleAsync(GetOrderDetailQuery request)
    {
        var order = await _dbContext.Orders
            .Include(x => x.Buyer)
            .Include(x => x.Seller)
            .FirstOrDefaultAsync((x) =>
                request.IsBuyer ? x.BuyerId == request.UserId : x.SellerId == request.UserId);
        
        if(order == null)
            throw Failure.NotFound();

        return order.ToDetailModel(request.IsBuyer);
    }
}