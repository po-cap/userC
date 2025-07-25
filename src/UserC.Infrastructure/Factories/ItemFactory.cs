using UserC.Domain.Entities;
using UserC.Domain.Factories;
using UserC.Infrastructure.Services;

namespace UserC.Infrastructure.Factories;

public class ItemFactory : IItemFactory
{
    private readonly SnowflakeId _snowflakeId;

    public ItemFactory(SnowflakeId snowflakeId)
    {
        _snowflakeId = snowflakeId;
    }


    public Item WithoutSPU(
        string userId, 
        string description, 
        List<string> album, 
        List<SKU> skus)
    {
        return new Item
        {
            Id = _snowflakeId.Get(),
            UserId = userId,
            Description = description,
            Albums = album,
            Skus = skus,
            IsService = false
        };
    }
}