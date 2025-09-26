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

    public override Task SaveChangeAsync(Order entity)
    {
        if (entity.Reviews != null)
        {
            foreach (var review in entity.Reviews)
            {
                _dbContext.Add(review);
            }   
        }
        
        return base.SaveChangeAsync(entity);
    }
}