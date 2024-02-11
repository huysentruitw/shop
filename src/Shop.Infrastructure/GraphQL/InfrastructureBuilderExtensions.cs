using System.Reflection;
using FluentValidation;
using HotChocolate.Execution.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Shop.Infrastructure.GraphQL;

public static class InfrastructureBuilderExtensions
{
    public static InfrastructureBuilder AddGraphQL<TDbContext>(this InfrastructureBuilder builder)
        where TDbContext : DbContext
    {
        builder.Services
            .AddGraphQLServer()
            .AddAuthorization()
            .RegisterDbContext<TDbContext>(DbContextKind.Pooled)
            .AddProjections()
            .AddQueryType<Query>()
            .AddMutationType<Mutation>()
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
