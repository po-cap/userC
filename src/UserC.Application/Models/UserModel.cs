using UserC.Domain.Entities;

namespace UserC.Application.Models;

public class UserModel
{
    /// <summary>
    /// 使用者 ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 顯示名稱
    /// </summary>
    public string DisplayName { get; set; }
    
    /// <summary>
    /// 頭像
    /// </summary>
    public string Avatar { get; set; }
}



public static partial class UserExtension 
{
    public static UserModel ToModel(this User user)
    {
        return new UserModel()
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Avatar = user.Avatar
        };
    }
}