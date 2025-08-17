using Microsoft.EntityFrameworkCore;
using Po.Api.Response;
using Shared.Mediator.Interface;
using UserC.Application.Models;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Queries;

public class GetItemQuery : IRequest<ItemModel>
{
    public long ItemId { get; set; }
}

public class GetItemHandler : IRequestHandler<GetItemQuery, ItemModel>
{
    private readonly AppDbContext _dbContext;

    public GetItemHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ItemModel> HandleAsync(GetItemQuery request)
    {
        var item = await _dbContext.Items.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == request.ItemId);
        if(item == null)
            throw Failure.NotFound();
        
        return item.ToModel();
    }
}