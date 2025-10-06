using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Repositories;

namespace UserC.Application.Queries;

/// <summary>
/// 查看商品是否收藏過
/// </summary>
public class FavoriteStatusQuery : IRequest<bool>
{
    /// <summary>
    /// 商品 ID
    /// </summary>
    public long ItemId { get; set; }
}

public class FavoriteStatusQueryHandler : IRequestHandler<FavoriteStatusQuery, bool>
{
    private readonly IAuthorizeUser _user;
    private readonly IUserRepository _repository;

    public FavoriteStatusQueryHandler(
        IAuthorizeUser user,
        IUserRepository repository)
    {
        _user = user;
        _repository = repository;
    }


    public async Task<bool> HandleAsync(FavoriteStatusQuery request)
    {
        // 從倉庫中取出用戶資料
        var user = await _repository.GetByIdAsync(
            _user.Id, q => q.Include(x => x.Favorites));
        if(user == null)
            throw Failure.NotFound();


        return user.Favorites.Any(x => x.ItemId == request.ItemId);
    }
}