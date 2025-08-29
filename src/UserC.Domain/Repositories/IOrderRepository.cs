using UserC.Domain.Entities;

namespace UserC.Domain.Repositories;

public interface IOrderRepository
{
    void Add(Order order);
}