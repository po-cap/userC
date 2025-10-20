using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Models;
using UserC.Application.Services;
using UserC.Domain.Repositories;

namespace UserC.Application.Queries;

public class FollowQuery : IRequest<IEnumerable<UserModel>>
{
    /// <summary>
    /// 時間游標
    /// </summary>
    public DateTimeOffset? CursorTime { get; set; }

    /// <summary>
    /// 取幾筆資料
    /// </summary>
    public int? Size { get; set; }
}

public class FollowHandler : IRequestHandler<FollowQuery, IEnumerable<UserModel>>
{
    /// <summary>
    /// 認證用戶
    /// </summary>
    private readonly IAuthorizeUser _user;

    /// <summary>
    /// 使用者倉儲
    /// </summary>
    private readonly IUserRepository _userRepository;

    public FollowHandler(
        IAuthorizeUser user, 
        IUserRepository userRepository)
    {
        _user = user;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserModel>> HandleAsync(FollowQuery request)
    {
        // 取出使用者資訊
        var user = await _userRepository.GetByIdAsync(
            _user.Id, 
            q => q.Include(
                x => x.Followings
                .OrderByDescending(y => y.CreatedAt)
                .AsQueryable()));
        
        // 若沒有使用者資訊，代表 token 有問題，拋出 401 錯誤
        if (user == null)
            throw Failure.Unauthorized();
        
        // 取得關注者資料並返回
        return user.Followings
            .Where(x => x.CreatedAt > (request.CursorTime ?? DateTimeOffset.Now.AddMonths(-1)))
            .Take(request.Size ?? 20)
            .Select(x => x.Following.ToModel());
    }
}