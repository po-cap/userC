using System.Text.Json;
using UserC.Domain.Entities;
using UserC.Domain.Factories;
using UserC.Infrastructure.Services;

namespace UserC.Infrastructure.Factories;

public class SkuFactory : ISkuFactory
{
    private readonly SnowflakeId _snowflakeId;

    public SkuFactory(SnowflakeId snowflakeId)
    {
        _snowflakeId = snowflakeId;
    }


    public SKU Create(
        string name, 
        JsonDocument spec, 
        string? photo, 
        double price, 
        int quantity)
    {
        var sku = new SKU
        {
            Id = _snowflakeId.Get(),
            Name = name,
            Photo = photo,
            Specs = spec,
            Price = price,
            Inventories = [new Inventory()
            {
                Id = _snowflakeId.Get(),
                AvailableStock = quantity
            }]
        };
        return sku;
    }
}