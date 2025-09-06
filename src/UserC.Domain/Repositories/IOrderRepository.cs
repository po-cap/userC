using UserC.Domain.Entities;
using UserC.Domain.Entities.Orders;

namespace UserC.Domain.Repositories;

public interface IOrderRepository
{
    void Add(Order order);
}