using CleanArchitectureDemo.Application.Interfaces;
using CleanArchitectureDemo.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureDemo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Singleton - כי הנתונים נשמרים בזיכרון וצריכים להישאר בין בקשות
        services.AddSingleton<IProductRepository, InMemoryProductRepository>();
        return services;
    }
}
