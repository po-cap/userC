using Microsoft.EntityFrameworkCore;
using Shared.Mediator.Interface;
using UserC.Application.Models.Brief;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Queries.Items;


/// <summary>
/// 取得最新商品
/// </summary>
public class GetNewItemsQuery : IRequest<IEnumerable<BriefItemModel>>
{
    /// <summary>
    /// 上一頁最後一筆資料
    /// </summary>
    public long? LastId { get; set; }
    
    /// <summary>
    /// 取幾筆資料
    /// </summary>
    public int Size { get; set; }
}

public class GetNewItemsHandler : IRequestHandler<GetNewItemsQuery, IEnumerable<BriefItemModel>>
{
    private readonly AppDbContext _dbContext;

    public GetNewItemsHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<BriefItemModel>> HandleAsync(GetNewItemsQuery request)
    {
        var items = await _dbContext.Items.AsQueryable()
            .OrderByDescending(x => x.Id)
            .Include(x => x.User)
            .Where(x => request.LastId == null || x.Id < request.LastId)
            .Take(request.Size)
            .ToListAsync();

        return from i in items select i.ToBriefModel();
    }
}