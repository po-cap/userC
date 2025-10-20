using UserC.Domain.Entities;
using UserC.Domain.Entities.Items;
using UserC.Domain.Repositories;
using UserC.Infrastructure.Persistence;

namespace UserC.Infrastructure.Repositories;

public class ItemRepository : Repository<Item>, IItemRepository
{
    private readonly AppDbContext _dbContext;

    public ItemRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }


    public void Add(Item item)
    {
        _dbContext.Items.Add(item);
    }
}