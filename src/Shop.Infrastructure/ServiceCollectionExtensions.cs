using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Shop.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        Action<InfrastructureBuilder> buildAction)
    {
        var builder = new InfrastructureBuilder
        {
            ServiceAssembly = Assembly.GetCallingAssembly(),
            Services = services,
        };

        buildAction(builder);
        
        return services;
    }
}
