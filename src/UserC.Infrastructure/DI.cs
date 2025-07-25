using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserC.Application.Services;
using UserC.Domain.Entities;
using UserC.Domain.Factories;
using UserC.Domain.Repositories;
using UserC.Infrastructure.Factories;
using UserC.Infrastructure.Persistence;
using UserC.Infrastructure.Repositories;
using UserC.Infrastructure.Services;

namespace UserC.Infrastructure;

public static class DI
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfigurationManager config)
    {
        // description - 主資料庫配置
        services.AddDbContext<AppDbContext>(opts =>
        {
            opts.UseNpgsql(config.GetConnectionString("Main"), o =>
            {
                o.MapEnum<Status>("status");
            });
        });
        
        // description - Unit Of Work (Atomicity)
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // description - snowflake id
        services.AddSingleton<SnowflakeId>(provider => 
            new SnowflakeId(workerId: 1, datacenterId: 1)
        );
        
        // description - factories
        services.AddScoped<ISkuFactory, SkuFactory>();
        services.AddScoped<IItemFactory, ItemFactory>();
        
        // description - repositories
        services.AddScoped<IItemRepository, ItemRepository>();
        
        return services;
    }
}