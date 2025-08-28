using Microsoft.EntityFrameworkCore;
using Shared.Mediator.Interface;
using UserC.Application.Models;
using UserC.Domain.Entities;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Queries;

public class GetItemSkusQuery : IRequest<IEnumerable<SkuModel>>
{
    public long ItemId { get; set; }
}

public class GetItemSkusHandler : IRequestHandler<GetItemSkusQuery, IEnumerable<SkuModel>>
{
    private readonly AppDbContext _dbContext;

    public GetItemSkusHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IEnumerable<SkuModel>> HandleAsync(GetItemSkusQuery request)
    {
        return await _dbContext.SKUs.Where(x => x.ItemId == request.ItemId).Select(x => x.ToModel()).ToListAsync();
    }
}