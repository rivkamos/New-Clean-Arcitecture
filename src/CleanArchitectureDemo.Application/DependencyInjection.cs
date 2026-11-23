using CleanArchitectureDemo.Application.Interfaces;
using CleanArchitectureDemo.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureDemo.Application;

// כל שכבה רושמת את השירותים שלה בעצמה
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        return services;
    }
}
