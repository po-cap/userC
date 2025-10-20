using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Entities;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Users;

public class FollowCmd : IRequest
{
    /// <summary>
    /// 被關注者 ID
    /// </summary>
    public long FollowingId { get; set; }
}

public class FollowHandler : IRequestHandler<FollowCmd>
{
    /// <summary>
    /// 使用者資料倉儲
    /// </summary>
    private readonly IUserRepository _repository;

    /// <summary>
    /// 認證使用者
    /// </summary>
    private readonly IAuthorizeUser _user;

    
    public FollowHandler(
        IUserRepository repository, 
        IAuthorizeUser user)
    {
        _repository = repository;
        _user = user;
    }


    public async Task HandleAsync(FollowCmd request)
    {
        // 獲取使用者資訊
        var user = await _repository.GetByIdAsync(
            _user.Id,
            q => q.Include(x => x.Followings));
        if(user == null)
            throw Failure.NotFound();

        // 查看是否已關注用戶
        // 若未關注，關注；若關注過，取消關注
        var following = user.Followings.FirstOrDefault(x => x.FollowingId == request.FollowingId);
        if (following == null)
        {
            user. Followings.Add(new Follow()
            {
                FollowerId = _user.Id,
                FollowingId = request.FollowingId,
                CreatedAt = DateTimeOffset.Now
            }); 
        }
        else
        {
            user.Followings.Remove(following);
        }

        // 存取變更
        await _repository.SaveChangeAsync(user);
    }
}