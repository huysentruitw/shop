using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Infrastructure.EntityFramework.Interceptors;

namespace Shop.Infrastructure.EntityFramework;

public static class InfrastructureBuilderExtensions
{
    public static InfrastructureBuilder AddEntityFramework<TDbContext>(this InfrastructureBuilder builder, string connectionString)
        where TDbContext : DbContext
    {
        builder.Services.AddPooledDbContextFactory<TDbContext>(
            options => options
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .AddInterceptors(new SaveChangesInterceptor())
#if DEBUG
                .EnableSensitiveDataLogging()
#endif
                .UseSqlServer(connectionString),
            poolSize: 1024);

        return builder;
    }
}
