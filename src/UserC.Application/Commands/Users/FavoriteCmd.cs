using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Users;

public class FavoriteCmd : IRequest
{
    /// <summary>
    /// 商品 ID
    /// </summary>
    public long ItemId { get; set; }
}

public class FavoriteHandler : IRequestHandler<FavoriteCmd>
{
    /// <summary>
    /// 認證用戶
    /// </summary>
    private readonly IAuthorizeUser _user;
    
    /// <summary>
    /// 用戶倉庫
    /// </summary>
    private readonly IUserRepository _repository;

    public FavoriteHandler(
        IAuthorizeUser user, 
        IUserRepository repository)
    {
        _user = user;
        _repository = repository;
    }
    
    public async Task HandleAsync(FavoriteCmd request)
    {
        // explain
        var user = await _repository.GetByIdAsync(
            _user.Id, 
            q => q.Include(x => x.Favorites));
        if(user == null)
            throw Failure.NotFound();

        // 如果已經收藏了，就取消收藏
        if (user.Favorites.FirstOrDefault(x => x.ItemId == request.ItemId) != null)
        {
            user.RemoveFromFavorite(itemId: request.ItemId, userId: user.Id);
        }
        // 如果還沒收藏，就收藏
        else
        {
            user.AddToFavorite(itemId: request.ItemId, userId: user.Id);
        }

        // explain
        await _repository.SaveChangeAsync(user);
    }
}