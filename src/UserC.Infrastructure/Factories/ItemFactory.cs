using System.Text.Json;
using UserC.Application.Services;
using UserC.Domain.Entities;
using UserC.Domain.Entities.Items;
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


    public Item New(
        long userId, 
        string description, 
        List<string> album, 
        List<SKU> skus,
        JsonDocument assets,
        double shippingFee,
        bool isVideo)
    {
        var id = _snowflake.Get();
        
        return new Item()
        {
            Id = id,
            UserId = userId,
            Description = description,
            ShippingFee = shippingFee,
            DeListed = false,
            Extra = assets,
            Album = new Album()
            {
                ItemId = id,
                Assets = album,
                IsVideo = isVideo
            },
            Skus = skus
        };
    }
}