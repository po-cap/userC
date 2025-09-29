namespace UserC.Domain.Entities;

public class Follow
{
    /// <summary>
    /// 關注者 ID
    /// </summary>
    public long FollowerId { get; set; }

    /// <summary>
    /// 被關注者 ID
    /// </summary>
    public long FollowingId { get; set; }
    
    /// <summary>
    /// 建立時間
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Navigation Property - 被關注者
    /// </summary>
    public User Following { get; set; }
}