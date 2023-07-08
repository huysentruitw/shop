using System.Reflection;
using FluentValidation;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shop.Infrastructure.GraphQL;

public static class InfrastructureBuilderExtensions
{
    public static InfrastructureBuilder AddGraphQL(this InfrastructureBuilder builder)
    {
        builder.Services
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .AddFairyBread()
            .AddValidatorsFromAssembly(builder.ServiceAssembly)
            .AddTypeExtensionsFromAssembly(builder.ServiceAssembly);
        
        return builder;
    }

    private static IRequestExecutorBuilder AddTypeExtensionsFromAssembly(this IRequestExecutorBuilder builder, Assembly assembly)
    {
        var types = assembly
            .GetTypes()
            .Where(x => 
                x.GetCustomAttribute(typeof(ExtendObjectTypeAttribute)) is not null ||
                x.GetCustomAttribute(typeof(ExtendObjectTypeAttribute<>)) is not null);

        foreach (var type in types)
            builder.AddTypeExtension(type);
        
        return builder;
    }

    private static IRequestExecutorBuilder AddValidatorsFromAssembly(this IRequestExecutorBuilder builder, Assembly assembly)
    {
        builder.Services.AddValidatorsFromAssembly(
            assembly,
            lifetime: ServiceLifetime.Singleton,
            includeInternalTypes: true);
        
        return builder;
    }
}
