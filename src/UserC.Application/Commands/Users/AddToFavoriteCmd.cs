using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Users;

public class AddToFavoriteCmd : IRequest
{
    /// <summary>
    /// 商品 ID
    /// </summary>
    public long ItemId { get; set; }
}

public class AddToFavoriteHandler : IRequestHandler<AddToFavoriteCmd>
{
    /// <summary>
    /// 認證用戶
    /// </summary>
    private readonly IAuthorizeUser _user;
    
    /// <summary>
    /// 用戶倉庫
    /// </summary>
    private readonly IUserRepository _repository;

    public AddToFavoriteHandler(
        IAuthorizeUser user, 
        IUserRepository repository)
    {
        _user = user;
        _repository = repository;
    }
    
    public async Task HandleAsync(AddToFavoriteCmd request)
    {
        // explain
        var user = await _repository.GetByIdAsync(_user.Id, q => q.Include(x => x.Favorites));
        if(user == null)
            throw Failure.NotFound();
        
        // explain
        user.AddToFavorite(itemId: request.ItemId, userId: user.Id);

        // explain
        await _repository.SaveChangeAsync(user);
    }
}