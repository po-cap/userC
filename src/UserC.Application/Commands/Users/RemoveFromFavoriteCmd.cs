using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Services;
using UserC.Domain.Repositories;

namespace UserC.Application.Commands.Users;

public class RemoveFromFavoriteCmd : IRequest
{
    /// <summary>
    /// 商品 ID
    /// </summary>
    public long ItemId { get; set; }
}

public class RemoveFromFavoriteHandler : IRequestHandler<RemoveFromFavoriteCmd>
{
    private readonly IAuthorizeUser _user;

    private readonly IUserRepository _repository;

    public RemoveFromFavoriteHandler(
        IAuthorizeUser user, 
        IUserRepository repository)
    {
        _user = user;
        _repository = repository;
    }


    public async Task HandleAsync(RemoveFromFavoriteCmd request)
    {
        // explain
        var user = await _repository.GetByIdAsync(_user.Id, q => q.Include(x => x.Favorites));
        if (user == null)
            throw Failure.Forbidden();
        
        // explain
        user.RemoveFromFavorite(
            itemId: request.ItemId,
            userId: _user.Id);

        // explain
        await _repository.SaveChangeAsync(user);
    }
}