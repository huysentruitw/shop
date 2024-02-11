namespace Shop.Application.Features.Organization.OutputTypes;

public sealed record Organization
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public required bool IsAdmin { get; init; }
}
