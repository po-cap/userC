using System.Linq.Expressions;
using UserC.Domain.Entities;
using UserC.Domain.Entities.Orders;

namespace UserC.Domain.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    void Add(Order order);
}