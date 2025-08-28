using System.Text.Json;
using UserC.Application.Services;
using UserC.Domain.Entities;
using UserC.Domain.Factories;
using UserC.Infrastructure.Services;

namespace UserC.Infrastructure.Factories;

public class SkuFactory : ISkuFactory
{
    private readonly Snowflake _snowflake;

    public SkuFactory(Snowflake snowflake)
    {
        _snowflake = snowflake;
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
            Id = _snowflake.Get(),
            Name = name,
            Photo = photo,
            Specs = spec,
            Price = price,
            AvailableStock = quantity,
            AllocatedStock = 0,
            //Inventories = [new Inventory()
            //{
            //    Id = _snowflake.Get(),
            //    AvailableStock = quantity
            //}]
        };
        return sku;
    }
}