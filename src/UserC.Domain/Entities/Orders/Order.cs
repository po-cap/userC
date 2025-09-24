using System.Text.Json;
using Po.Api.Response;
using UserC.Domain.Entities.Items;
using UserC.Domain.Entities.Rating;
using UserC.Domain.Enums;

namespace UserC.Domain.Entities.Orders;

public class Order
{
    /// <summary>
    /// ID
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// 買家 ID
    /// </summary>
    public long BuyerId { get; set; }
    
    /// <summary>
    /// 賣家 ID
    /// </summary>
    public long SellerId { get; set; }
    
    /// <summary>
    /// Item ID
    /// </summary>
    public long ItemId { get; set; }
    
    /// <summary>
    /// 狀態
    /// </summary>
    public OrderStatus Status { get; set; }
    
    /// <summary>
    /// 快照
    /// </summary>
    public JsonDocument Snapshot { get; set; }

    /// <summary>
    /// 被買家評論過
    /// </summary>
    public bool ReviewedByBuyer { get; set; }
    
    /// <summary>
    /// 被賣家評論過
    /// </summary>
    public bool ReviewedBySeller { get; set; }

    /// <summary>
    /// Navigation Property - 明細
    /// </summary>
    public OrderAmount Amount { get; set; }

    /// <summary>
    /// Navigation Property - 記事
    /// </summary>
    public OrderRecord Record { get; set; }

    /// <summary>
    /// Navigation Property - 貨運資訊
    /// </summary>
    public OrderShipment Shipment { get; set; }

    /// <summary>
    /// Navigation Property - 付款資訊
    /// </summary>
    public Payment Payment { get; set; }
    
    /// <summary>
    /// Navigation Property - 買家
    /// </summary>
    public User Buyer { get; set; }
    
    /// <summary>
    /// Navigation Property - 賣家
    /// </summary>
    public User Seller { get; set; }
    
    /// <summary>
    /// Navigation Property - 商品鏈結
    /// </summary>
    public Item Item { get; set; }

    /// <summary>
    /// Navigation Property - 評論
    /// </summary>
    public ICollection<Review> Reviews { get; internal set; }
    
    /// <summary>
    /// 評論
    /// </summary>
    /// <param name="id">評論 ID</param>
    /// <param name="user">評論者 ID</param>
    /// <param name="rating">評分</param>
    /// <param name="comment">評語</param>
    public Review OnReview(long id, User user, int rating, string comment)
    {
        if (Status < OrderStatus.delivered)
            throw Failure.BadRequest("貨物必須送達或取消訂單才能評論");
        
        var isBuyer = BuyerId == user.Id;

        Review review;
        // 新增評論
        if (Reviews.Count(x => x.IsBuyer == isBuyer) == 0)
        {
            review = new Review()
            {
                Id = id,
                ReviewerAvatar = user.Avatar,
                ReviewerDisplayName = user.DisplayName,
                IsBuyer = BuyerId == user.Id,
                OrderId = Id,
                UserId = isBuyer ? SellerId : BuyerId,
                Rating = rating,
                Comment = comment,
                CreatedAt = DateTimeOffset.Now
            };
            
            Reviews.Add(review);

            if (isBuyer)
                ReviewedByBuyer = true;
            else
                ReviewedBySeller = true;
        }
        // 修改評論
        else
        {
            review = Reviews.First(x => x.IsBuyer == isBuyer);

            review.Rating = rating;
            review.Comment = comment;
        }

        // 是否更改狀態
        if (Reviews.Count() >= 2)
        {
            Status = OrderStatus.completed;
        }

        return review;
    }
}