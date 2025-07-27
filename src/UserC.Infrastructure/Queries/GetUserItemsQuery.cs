using Microsoft.EntityFrameworkCore;
using Shared.Mediator.Interface;
using UserC.Application.Models;
using UserC.Domain.Entities;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Queries;

public class GetUserItemsQuery : IRequest<List<ItemModel>>
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



public class GetUserItemsHandler : IRequestHandler<GetUserItemsQuery, List<ItemModel>>
{
    private readonly AppDbContext _dbContext;

    public GetUserItemsHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ItemModel>> HandleAsync(GetUserItemsQuery request)
    {
        var items = _dbContext.Items.AsQueryable();

        items = items.Where(x => x.UserId == request.UserId)
            .OrderByDescending(x => x.Id)
            .Include(x => x.User)
            .Where(x => request.LastId == null || x.Id < request.LastId)
            .Take(request.Size);

        return await (from i in items select i.ToModel()).ToListAsync();
    }
}