namespace Shop.Application.Features.Catalog;

[ExtendObjectType<Mutation>]
public sealed class AddProductMutation
{
    public async Task<MutationResult> AddProduct(DataContext dataContext, AddProductInput input)
    {
        var product = new Entities.Product
        {
            Id = Guid.NewGuid(),
            Name = input.Name,
            UnitPrice = input.UnitPrice,
        };

        await dataContext.AddAsync(product);
        await dataContext.SaveChangesAsync();
        
        return MutationResult.ForSubject(product.Id);
    }
}

public sealed record AddProductInput
{
    public required string Name { get; init; }
    
    public required decimal UnitPrice { get; init; }
}

internal sealed class AddProductInputValidator : AbstractValidator<AddProductInput>
{
    public AddProductInputValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(Entities.Product.MaxNameLength);
        RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0);
    }
}
