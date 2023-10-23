using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Shop.Infrastructure.EntityFramework;

public static class InfrastructureBuilderExtensions
{
    public static InfrastructureBuilder AddEntityFramework<TDbContext>(this InfrastructureBuilder builder, string connectionString)
        where TDbContext : DbContext
    {
        builder.Services.AddPooledDbContextFactory<TDbContext>(
            options => options
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
#if DEBUG
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information)
#endif
                .UseSqlServer(connectionString),
            poolSize: 1024);

        return builder;
    }
}
