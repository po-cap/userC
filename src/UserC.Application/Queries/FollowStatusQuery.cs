using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Repositories;

namespace UserC.Application.Queries;

public class FollowStatusQuery : IRequest<bool>
{
    /// <summary>
    /// 被關注者 ID
    /// </summary>
    public long FollowingId { get; set; }
}

public class FollowStatusQueryHandler : IRequestHandler<FollowStatusQuery, bool>
{
    /// <summary>
    /// 認證用戶
    /// </summary>
    private readonly IAuthorizeUser _user;
    
    /// <summary>
    /// 使用者倉儲
    /// </summary>
    private readonly IUserRepository _repository;

    public FollowStatusQueryHandler(
        IAuthorizeUser user, 
        IUserRepository repository)
    {
        _user = user;
        _repository = repository;
    }

    public async Task<bool> HandleAsync(FollowStatusQuery request)
    {
        // 從倉庫中取出用戶資料
        var user = await _repository.GetByIdAsync(
            _user.Id, q => q.Include(x => x.Followings));
        if(user == null)
            throw Failure.NotFound();


        return user.Favorites.Any(x => x.ItemId == request.FollowingId);
    }
}