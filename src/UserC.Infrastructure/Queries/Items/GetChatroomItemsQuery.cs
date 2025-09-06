using Microsoft.EntityFrameworkCore;
using Shared.Mediator.Interface;
using UserC.Application.Models.Brief;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Queries.Items;

/// <summary>
/// 取得聊天室對應商品鏈結
/// </summary>
public class GetChatroomItemsQuery : IRequest<IEnumerable<BriefItemModel>>
{
    public IEnumerable<long> Ids { get; set; }
}

public class GetChatroomItemsHandler : IRequestHandler<GetChatroomItemsQuery, IEnumerable<BriefItemModel>>
{
    private readonly AppDbContext _context;

    public GetChatroomItemsHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BriefItemModel>> HandleAsync(GetChatroomItemsQuery request)
    {
        var entities = await _context.Items
            .Include(x => x.User)
            .Where(x => request.Ids.Contains(x.Id))
            .ToListAsync();
        
        return from entity in entities select entity.ToBriefModel();
    }
}