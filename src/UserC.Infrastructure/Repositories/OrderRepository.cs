using UserC.Domain.Entities;
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
}