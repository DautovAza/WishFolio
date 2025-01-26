namespace WishFolio.Application.UseCases.WishListItemsReservations.Commands.Abstractions;

public interface IReservationItemCommand
{
    Guid ItemId { get; }
}
