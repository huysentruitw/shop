using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Shop.Infrastructure;

public sealed record InfrastructureBuilder
{
    public required Assembly ServiceAssembly { get; init; }
    
    public required IServiceCollection Services { get; init; }
}
