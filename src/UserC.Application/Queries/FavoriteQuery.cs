using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Models.Brief;
using UserC.Application.Services;
using UserC.Domain.Repositories;

namespace UserC.Application.Queries;
 
/// <summary>
/// 获取我的收藏列表(或是否已收藏某商品)
/// </summary>
public class FavoriteQuery : IRequest<IEnumerable<BriefItemModel>>
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

public class FavoriteQueryHandler : IRequestHandler<FavoriteQuery, IEnumerable<BriefItemModel>>
{
    private readonly IAuthorizeUser _user;

    private readonly IOrderRepository _itemRepository;

    private readonly IUserRepository _userRepository;

    public FavoriteQueryHandler(
        IAuthorizeUser user, 
        IOrderRepository itemRepository, 
        IUserRepository userRepository)
    {
        _user = user;
        _itemRepository = itemRepository;
        _userRepository = userRepository;
    }


    public async Task<IEnumerable<BriefItemModel>> HandleAsync(FavoriteQuery request)
    {
        // explain
        var user = await _userRepository.GetByIdAsync(_user.Id, q => q
            .Include(x => x.Favorites
                .OrderByDescending(y => y.CreatedAt)
                .AsQueryable())
            .ThenInclude(x => x.Item));
        
        // explain
        if(user == null)
            throw Failure.NotFound();

        // explain
        return user.Favorites
            .Where(x => x.CreatedAt > (request.CursorTime ?? DateTimeOffset.Now.AddMonths(-1)))
            .Take(request.Size ?? 20)
            .Select(x => x.Item.ToBriefModel());
    }
}