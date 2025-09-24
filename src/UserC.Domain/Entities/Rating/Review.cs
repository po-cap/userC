using UserC.Domain.Entities.Orders;

namespace UserC.Domain.Entities.Rating;

public class Review
{
    /// <summary>
    /// ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 被評價者 ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 被評價傷品 ID
    /// </summary>
    public long OrderId { get; set; }

    /// <summary>
    /// 被評價的人是否為買家
    /// </summary>
    public bool IsBuyer { get; set; }

    /// <summary>
    /// 評語者頭像
    /// </summary>
    public string ReviewerAvatar { get; set; }

    /// <summary>
    /// 評語者顯示名稱
    /// </summary>
    public string ReviewerDisplayName { get; set; }

    /// <summary>
    /// 評分
    /// 1: 差評
    /// 2: 不好不壞
    /// 3: 好評
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// 評價
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// 評價時間
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
}