using System.Text.Json;
using UserC.Domain.Entities;

namespace UserC.Domain.Factories;

public interface ISkuFactory
{
    SKU Create(
        string name, 
        JsonDocument spec,
        string? photo,
        double price, 
        int quantity);
}