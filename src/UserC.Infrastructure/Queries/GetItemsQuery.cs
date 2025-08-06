using Microsoft.EntityFrameworkCore;
using Shared.Mediator.Interface;
using UserC.Application.Models;
using UserC.Domain.Entities;
using UserC.Domain.Repositories;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Queries;

public class GetItemsQuery : IRequest<IEnumerable<ItemModel>>
{
    public IEnumerable<long> Ids { get; set; }
}

public class GetItemsHandler : IRequestHandler<GetItemsQuery, IEnumerable<ItemModel>>
{
    private readonly AppDbContext _context;

    public GetItemsHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ItemModel>> HandleAsync(GetItemsQuery request)
    {
        var entities = await _context.Items
            .Include(x => x.User)
            .Where(x => request.Ids.Contains(x.Id))
            .ToListAsync();
        
        return from entity in entities select entity.ToModel();
    }
}