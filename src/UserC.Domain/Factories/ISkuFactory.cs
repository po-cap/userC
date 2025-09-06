using System.Text.Json;
using UserC.Domain.Entities;
using UserC.Domain.Entities.Items;

namespace UserC.Domain.Factories;

public interface ISkuFactory
{
    SKU Create(
        string name, 
        JsonDocument spec,
        double price, 
        int quantity);
}