using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Shop.Infrastructure.EntityFramework;

public static class InfrastructureBuilderExtensions
{
    public static InfrastructureBuilder AddEntityFramework(this InfrastructureBuilder builder, string connectionString)
    {
        builder.Services.AddDbContextFactory<DataContext>(options =>
            options.UseMySql(connectionString, ServerVersion.Parse("8.0.31")));
        
        return builder;
    }
}
