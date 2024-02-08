using Shop.Application.Exceptions;

namespace Shop.Application.Features.Basket;

[ExtendObjectType<Mutation>]
public sealed class UpdateBasketReservationMutation
{
    public async Task<MutationResult> UpdateBasketReservation(DataContext dataContext, UpdateBasketReservationInput input)
    {
        var basket = await dataContext.FindAsync<Entities.Basket>(input.BasketId)
            ?? throw new BasketNotFoundException(input.BasketId);

        var reservation = basket.Reservations.SingleOrDefault(x => x.ProductId == input.ProductId)
            ?? throw new BasketReservationNotFoundException(input.ProductId);

        reservation.Quantity = input.Quantity;
        dataContext.Update(basket);

        await dataContext.SaveChangesAsync();

        return MutationResult.ForSubject(basket.Id);
    }
}

public sealed record UpdateBasketReservationInput
{
    public required Guid BasketId { get; init; }

    public required Guid ProductId { get; init; }

    public required int Quantity { get; init; }
}

internal sealed class UpdateBasketReservationInputValidator : AbstractValidator<UpdateBasketReservationInput>
{
    public UpdateBasketReservationInputValidator()
    {
        RuleFor(x => x.BasketId).NotEmpty();
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
    }
}
