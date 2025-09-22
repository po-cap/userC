using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using UserC.Domain.Entities.Orders;
using UserC.Domain.Enums;
using UserC.Domain.Repositories;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    private readonly AppDbContext _dbContext;

    public OrderRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Order order)
    {
        _dbContext.Orders.Add(order);
    }
    
    public async Task MarkAsPaidAsync(long id)
    {
        var order = await _dbContext
            .Orders
            .Include(x => x.Record)
            .FirstAsync(x => x.Id == id);

        order.Status = OrderStatus.paid;
        order.Record.PaidAt = DateTimeOffset.Now;
    }

    public async Task MarkAsShippedAsync(
        long id, 
        long userId, 
        string ShippingProvider, 
        string trackingNumber)
    {
        var order = await _dbContext
            .Orders
            .Include(x => x.Record)
            .Include(x => x.Shipment)
            .FirstAsync(x => x.Id == id);
        
        if(order.SellerId != userId)
            throw Failure.Unauthorized();

        // 如果已取到貨，設定物流單號就沒有意義了
        if (order.Record.DeliveredAt != null)
            return;

        order.Status = OrderStatus.delivered;
        order.Record.ShippedAt = DateTimeOffset.Now;
        order.Shipment.ShippingProvider = ShippingProvider;
        order.Shipment.TrackingNumber = trackingNumber;
    }

    public async Task MarkAsDeliveredAsync(long id, long userId)
    {
        var order = await _dbContext
            .Orders
            .Include(x => x.Record)
            .Include(x => x.Shipment)
            .FirstAsync(x => x.Id == id);
        
        // 只有買家可以設定已取貨
        if(order.BuyerId != userId)
            throw Failure.Unauthorized();

        order.Status = OrderStatus.delivered;
        order.Record.DeliveredAt = DateTimeOffset.Now;
    }
}