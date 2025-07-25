using UserC.Domain.Entities;

namespace UserC.Domain.Repositories;

public interface IItemRepository
{
    void Add(Item item);
}