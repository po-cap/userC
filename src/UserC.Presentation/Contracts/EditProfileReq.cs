using UserC.Application.Commands.Users;
using UserC.Presentation.Utilities;

namespace UserC.Presentation.Contracts;

public class EditProfileReq
{
    /// <summary>
    /// 顯示名稱
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// 頭像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 橫幅
    /// </summary>
    public string? Banner { get; set; }
}


public static partial class ContractExtension
{
    public static EditUserCommand ToCommand(this EditProfileReq request, HttpContext context)
    {
        return new EditUserCommand()
        {
            Id = context.UserID(),
            DisplayName = request.DisplayName,
            Avatar = request.Avatar,
            Banner = request.Banner
        };
    }
}