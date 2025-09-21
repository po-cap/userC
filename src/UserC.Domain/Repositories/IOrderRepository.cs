using UserC.Domain.Entities;
using UserC.Domain.Entities.Orders;

namespace UserC.Domain.Repositories;

public interface IOrderRepository
{
    void Add(Order order);

    /// <summary>
    /// 設定為已付款
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task MarkAsPaid(long id);
}