using UserC.Domain.Entities;
using UserC.Domain.Entities.Orders;
using UserC.Domain.Entities.Rating;

namespace UserC.Domain.Repositories;

public interface IReviewRepository : IRepository<Review>
{
    /// <summary>
    /// 新增一筆評論
    /// </summary>
    /// <param name="order">被評論訂單</param>
    /// <param name="user">被評論者</param>
    /// <param name="rating">評價分數</param>
    /// <param name="comment">評語</param>
    Task<Review> AddAsync(Order order, User user, int rating, string comment);
}