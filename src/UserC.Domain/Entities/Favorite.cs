using UserC.Domain.Entities.Items;

namespace UserC.Domain.Entities;

public class Favorite
{
    /// <summary>
    /// 使用者 ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 商品 ID
    /// </summary>
    public long ItemId { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Navigation Property - 商品
    /// </summary>
    public Item Item { get; set; }
}