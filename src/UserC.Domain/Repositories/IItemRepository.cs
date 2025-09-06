using UserC.Domain.Entities;
using UserC.Domain.Entities.Items;

namespace UserC.Domain.Repositories;

public interface IItemRepository
{
    void Add(Item item);
}