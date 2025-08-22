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
}