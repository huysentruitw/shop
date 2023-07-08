using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Shop.Infrastructure.EntityFramework;

public static class InfrastructureBuilderExtensions
{
    public static InfrastructureBuilder AddEntityFramework<TDbContext>(this InfrastructureBuilder builder, string connectionString)
        where TDbContext : DbContext
    {
        builder.Services.AddPooledDbContextFactory<TDbContext>(
            options => options
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .UseMySql(connectionString, ServerVersion.Parse("8.0.31")),
            poolSize: 1024);
        
        return builder;
    }
}
