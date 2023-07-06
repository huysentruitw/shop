namespace Shop.Application.Features;

[ExtendObjectType<Query>]
public sealed class PingQuery
{
    public string Ping => "Pong";
}
