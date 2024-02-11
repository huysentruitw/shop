namespace Shop.Application.Features.Catalog;

[ExtendObjectType<Mutation>]
public sealed class RemoveProductMutation
{
    public async Task<MutationResult> RemoveProduct(DataContext dataContext, RemoveProductInput input)
    {
        var product = await dataContext.FindAsync<Entities.Product>(input.Id)
            ?? throw new ProductNotFoundException(input.Id);

        dataContext.Remove(product);
        await dataContext.SaveChangesAsync();
        
        return MutationResult.ForSubject(product.Id);
    }
}

public sealed record RemoveProductInput
{
    public required Guid Id { get; init; }
}
