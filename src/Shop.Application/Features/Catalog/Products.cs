namespace Shop.Application.Features.Catalog;

[ExtendObjectType<Query>]
public sealed class ProductsQuery
{
    [UsePaging]
    [UseProjection]
    public QueryableExecutable<OutputTypes.Product> Products(DataContext dataContext)
        => dataContext.Set<Entities.Product>()
            .OrderBy(x => x.Name)
            .Select(x => new OutputTypes.Product
            {
                Id = x.Id,
                Name = x.Name,
                UnitPrice = x.UnitPrice,
            })
            .AsExecutable();
}
