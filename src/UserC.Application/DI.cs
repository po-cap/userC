using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Po.Media;
using Shared.Mediator;

namespace UserC.Application;

public static class DI
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // 注入多媒體工具
        services.AddMediaService(configuration);
        
        // 注入 mediator
        services.AddMediator();
        
        return services;
    }
}