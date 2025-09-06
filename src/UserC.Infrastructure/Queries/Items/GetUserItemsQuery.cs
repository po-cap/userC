using Microsoft.EntityFrameworkCore;
using Shared.Mediator.Interface;
using UserC.Application.Models.Brief;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Queries.Items;

public class GetUserItemsQuery : IRequest<IEnumerable<BriefItemModel>>
{
    /// <summary>
    /// 使用者 ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 上一頁最後一筆資料
    /// </summary>
    public long? LastId { get; set; }
    
    /// <summary>
    /// 取幾筆資料
    /// </summary>
    public int Size { get; set; }
}



public class GetUserItemsHandler : IRequestHandler<GetUserItemsQuery, IEnumerable<BriefItemModel>>
{
    private readonly AppDbContext _dbContext;

    public GetUserItemsHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<BriefItemModel>> HandleAsync(GetUserItemsQuery request)
    {
        var items = await _dbContext.Items.AsQueryable()
            .Where(x => x.UserId == request.UserId)
            .OrderByDescending(x => x.Id)
            .Include(x => x.User)
            .Where(x => request.LastId == null || x.Id < request.LastId)
            .Take(request.Size)
            .ToListAsync();

        return from i in items select i.ToBriefModel();
    }
}