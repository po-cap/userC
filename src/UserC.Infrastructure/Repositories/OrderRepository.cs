using Microsoft.EntityFrameworkCore;
using UserC.Domain.Entities;
using UserC.Domain.Entities.Orders;
using UserC.Domain.Enums;
using UserC.Domain.Repositories;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _dbContext;

    public OrderRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Order order)
    {
        _dbContext.Orders.Add(order);
    }
    
    public async Task MarkAsPaid(long id)
    {
        var order = await _dbContext
            .Orders
            .Include(x => x.Record)
            .FirstAsync(x => x.Id == id);

        order.Status = OrderStatus.paid;
        order.Record.PaidAt = DateTimeOffset.Now;
    }
}