using System.Text.Json;
using UserC.Application.Services;
using UserC.Domain.Entities;
using UserC.Domain.Factories;
using UserC.Infrastructure.Services;

namespace UserC.Infrastructure.Factories;

public class ItemFactory : IItemFactory
{
    private readonly Snowflake _snowflake;

    public ItemFactory(Snowflake snowflake)
    {
        _snowflake = snowflake;
    }


    public Item WithoutSPU(
        long userId, 
        string description, 
        List<string> album, 
        List<SKU> skus,
        JsonDocument spec,
        double shippingFee)
    {
        return new Item
        {
            Id = _snowflake.Get(),
            UserId = userId,
            Description = description,
            Albums = album,
            Skus = skus,
            IsService = false,
            Specs = spec
        };
    }
}