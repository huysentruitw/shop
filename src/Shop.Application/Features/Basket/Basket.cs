using HotChocolate.Data;
using Microsoft.EntityFrameworkCore;

namespace Shop.Application.Features.Basket;

[ExtendObjectType<Query>]
public sealed class BasketQuery
{
    [UseProjection]
    public QueryableExecutable<OutputTypes.Basket> Basket(DataContext dataContext, Guid basketId)
        => dataContext.Set<Entities.Basket>()
            .Include(x => x.Reservations)
            .Where(x => x.Id == basketId)
            .Select(x => new OutputTypes.Basket
            {
                Id = x.Id,
                Reservations = x.Reservations.Select(r => new OutputTypes.Reservation
                {
                    ProductId = r.ProductId,
                    Quantity = r.Quantity,
                }).ToList(),
            })
            .AsExecutable();
}
