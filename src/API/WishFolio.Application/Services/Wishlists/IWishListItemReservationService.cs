
namespace WishFolio.Application.Services.Wishlists;

public interface IWishListItemReservationService
{
    Task AcceptReservation(Guid itemId);
    Task CancelReservation(Guid itemId);
    Task ReserveItem(Guid itemId, bool isAnonymous);
}