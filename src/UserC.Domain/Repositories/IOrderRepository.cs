using System.Linq.Expressions;
using UserC.Domain.Entities;
using UserC.Domain.Entities.Orders;

namespace UserC.Domain.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    void Add(Order order);

    /// <summary>
    /// 設定為已付款
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task MarkAsPaidAsync(long id);
    
    /// <summary>
    /// 設定為已發貨
    /// </summary>
    /// <param name="id">訂單編號</param>
    /// <param name="userId">做動作的使用者的 ID</param>
    /// <param name="ShippingProvider">物流公司</param>
    /// <param name="trackingNumber">物流單號</param>
    /// <returns></returns>
    Task MarkAsShippedAsync(long id, long userId, string ShippingProvider, string trackingNumber);

    /// <summary>
    /// 設定為已收貨
    /// </summary>
    /// <param name="id">訂單編號</param>
    /// <param name="userId">做動作的使用者的 ID</param>
    /// <returns></returns>
    Task MarkAsDeliveredAsync(long id, long userId);
}