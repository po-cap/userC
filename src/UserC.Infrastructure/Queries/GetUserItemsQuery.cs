using Shared.Mediator.Interface;
using UserC.Domain.Entities;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Queries;

public class GetUserItemsQuery : IRequest<List<Item>>
{
    /// <summary>
    /// 使用者 ID
    /// </summary>
    public long UserId { get; set; }
}



public class GetUserItemsHandler : IRequestHandler<GetUserItemsQuery, List<Item>>
{
    private AppDbContext _dbContext;

    public GetUserItemsHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Item>> HandleAsync(GetUserItemsQuery request)
    {
        var items = _dbContext.Items.AsQueryable();

        items = items.Where(x => x.UserId == request.UserId);

        return Task.FromResult(items.ToList());
    }
}