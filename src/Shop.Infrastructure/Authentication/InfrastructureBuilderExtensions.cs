using Microsoft.Extensions.DependencyInjection;

namespace Shop.Infrastructure.Authentication;

public static class InfrastructureBuilderExtensions
{
    public static InfrastructureBuilder AddAuthentication(this InfrastructureBuilder builder)
    {
        builder.Services
            .AddSingleton<IPasswordHasher, PasswordHasher>();

        builder.Services
            .AddAuthentication();

        return builder;
    }
}
