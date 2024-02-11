namespace Shop.Application.Features.Basket;

[ExtendObjectType<Mutation>]
public sealed class AddReservationToBasketMutation
{
    public async Task<MutationResult> AddReservationToBasket(DataContext dataContext, AddReservationToBasketInput input)
    {
        var basket = await dataContext.Set<Entities.Basket>()
            .Include(x => x.Reservations)
            .SingleOrDefaultAsync(x => x.Id == input.BasketId);

        var reservation = new Entities.Reservation
        {
            ProductId = input.ProductId,
            Quantity = input.Quantity,
        };

        if (basket is null)
        {
            basket = CreateBasket(reservation);
            await dataContext.AddAsync(basket);
        }
        else
        {
            UpdateBasket(basket, reservation);
            dataContext.Update(basket);
        }

        await dataContext.SaveChangesAsync();

        return MutationResult.ForSubject(basket.Id);
    }

    private static Entities.Basket CreateBasket(Entities.Reservation reservation)
    {
        return new Entities.Basket
        {
            Id = Guid.NewGuid(),
            Reservations = new[] { reservation },
        };
    }

    private static void UpdateBasket(Entities.Basket basket, Entities.Reservation reservation)
    {
        var existingReservationForSameProduct = basket.Reservations.SingleOrDefault(x => x.ProductId == reservation.ProductId);
        if (existingReservationForSameProduct is not null)
            existingReservationForSameProduct.Quantity += reservation.Quantity;
        else
            basket.Reservations.Add(reservation);
    }
}

public sealed record AddReservationToBasketInput
{
    public required Guid? BasketId { get; init; }

    public required Guid ProductId { get; init; }

    public required int Quantity { get; init; }
}

internal sealed class AddReservationToBasketInputValidator : AbstractValidator<AddReservationToBasketInput>
{
    public AddReservationToBasketInputValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}
