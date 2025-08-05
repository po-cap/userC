using Microsoft.EntityFrameworkCore;
using Shared.Mediator.Interface;
using UserC.Application.Models;
using UserC.Domain.Entities;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Queries;

public class GetNewItemsQuery : IRequest<IEnumerable<ItemModel>>
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

public class GetNewItemsHandler : IRequestHandler<GetNewItemsQuery, IEnumerable<ItemModel>>
{
    private readonly AppDbContext _dbContext;

    public GetNewItemsHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<ItemModel>> HandleAsync(GetNewItemsQuery request)
    {
        var items = _dbContext.Items.AsQueryable();

        items = items
            .OrderByDescending(x => x.Id)
            .Include(x => x.User)
            .Where(x => request.LastId == null || x.Id < request.LastId)
            .Take(request.Size);

        return await (from i in items select i.ToModel()).ToListAsync();
    }
}