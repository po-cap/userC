using UserC.Domain.Entities;

namespace UserC.Application.Models;

public class UserModel
{
    /// <summary>
    /// 使用者 ID
    /// </summary>
    public required long Id { get; set; }

    /// <summary>
    /// 顯示名稱
    /// </summary>
    public required string DisplayName { get; set; }
    
    /// <summary>
    /// 頭像
    /// </summary>
    public required string Avatar { get; set; }

    /// <summary>
    /// 橫幅
    /// </summary>
    public required string? Banner { get; set; }
}



public static partial class UserExtension 
{
    public static UserModel ToModel(this User user)
    {
        return new UserModel()
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Avatar = user.Avatar,
            Banner = user.Banner
        };
    }
}