namespace UserC.Domain.Entities;

public class User
{
    /// <summary>
    /// ID
    /// </summary>
    public long Id { get; init; }
    
    /// <summary>
    /// 頭像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 使用者名稱
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// 橫幅
    /// </summary>
    public string? Banner { get; set; }

    /// <summary>
    /// Navigation Property - 收藏列表
    /// </summary>
    public ICollection<Favorite> Favorites { get; set; }

    /// <summary>
    /// 收藏
    /// </summary>
    public void AddToFavorite(long itemId, long userId)
    {
        // explain
        Favorites ??= [];
        // explain
        var favorite = new Favorite()
        {
            UserId = userId,
            ItemId = itemId,
            CreatedAt = DateTimeOffset.Now
        };
        // explain
        Favorites.Add(favorite);
    }

    /// <summary>
    /// 取消收藏
    /// </summary>
    public void RemoveFromFavorite(long itemId, long userId)
    {
        var favorite = Favorites.First(x => 
            x.ItemId == itemId && 
            x.UserId == userId);
        
        Favorites.Remove(favorite);
    }
}