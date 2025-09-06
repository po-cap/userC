using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Models.Detailed;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Queries.Items;

/// <summary>
/// 取得商品詳情
/// </summary>
public class GetDetailItemQuery : IRequest<DetailItemModel>
{
    public long ItemId { get; set; }
}

public class GetDetailItemHandler : IRequestHandler<GetDetailItemQuery, DetailItemModel>
{
    private readonly AppDbContext _dbContext;

    public GetDetailItemHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DetailItemModel> HandleAsync(GetDetailItemQuery request)
    {
        var item = await _dbContext.Items
            .Include(x => x.User)
            .Include(x => x.Skus)
            .FirstOrDefaultAsync(x => x.Id == request.ItemId);
        if(item == null)
            throw Failure.NotFound();
        
        return item.ToDetailModel();
    }
}