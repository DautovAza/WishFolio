
namespace WishFolio.Application.UseCases.Wishlists;

public interface IWishListItemReservationService
{
    Task AcceptReservation(Guid itemId);
    Task CancelReservation(Guid itemId);
    Task ReserveItem(Guid itemId, bool isAnonymous);
}