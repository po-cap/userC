using Microsoft.EntityFrameworkCore;
using Shared.Mediator.Interface;
using UserC.Application.Models;
using UserC.Application.Models.Brief;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Queries.Orders;

public class SellerGetOrdersQuery : IRequest<IEnumerable<BriefOrderModel>>
{
    public long UserId { get; set; }

    public bool IsBuyer { get; set; }

    public int Size { get; set; }

    public long? LastId { get; set; }
}


public class SellerGetOrdersHandler : IRequestHandler<SellerGetOrdersQuery, IEnumerable<BriefOrderModel>>
{
    private readonly AppDbContext _dbContext;

    public SellerGetOrdersHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async  Task<IEnumerable<BriefOrderModel>> HandleAsync(SellerGetOrdersQuery request)
    {
        var items = await _dbContext.Orders
            .Include(x => x.Buyer)
            .Include(x => x.Seller)
            .Where((x) => (request.IsBuyer ? x.BuyerId == request.UserId : x.SellerId == request.UserId) && 
                          x.Id > (request.LastId?? 0)) 
            .OrderByDescending(x => x.Id)
            .Take(request.Size)
            .ToListAsync();

        return from item in items select item.ToBriefModel(request.IsBuyer);
    }
}