using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories;

namespace WishFolio.Application.Services.Wishlists;

public class WishListItemReservationService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserRepository _userRepository;
    private readonly IWishListRepository _wishListRepository;

    public async Task ReserveItem(Guid itemId, bool isAnonymous)
    {
        var currentUser = await _currentUserService.GetCurrentUserAsync();
        var item = await _wishListRepository.GetWishListItemByIdAsync(itemId);

        if (item is null)
        {
            throw new KeyNotFoundException($"Предмет виш-листа с идентефикатором {itemId} не найден!");
        }

        item.ReserveItem(currentUser, isAnonymous);
        await _wishListRepository.SaveChangesAsync();
    }

    public async Task CancelReservation(Guid itemId)
    {
        var currentUser = await _currentUserService.GetCurrentUserAsync();
        var item = await _wishListRepository.GetWishListItemByIdAsync(itemId);

        if (item is null)
        {
            throw new KeyNotFoundException($"Предмет виш-листа с идентефикатором {itemId} не найден!");
        }

        item.CancelReservation(currentUser);
        await _wishListRepository.SaveChangesAsync();
    }

    public async Task AcceptReservation(Guid itemId)
    {
        var currentUser = await _currentUserService.GetCurrentUserAsync();
        var item = await _wishListRepository.GetWishListItemByIdAsync(itemId);

        if (item is null)
        {
            throw new KeyNotFoundException($"Предмет виш-листа с идентефикатором {itemId} не найден!");
        }

        item.CloseReservation(currentUser);
        await _wishListRepository.SaveChangesAsync();
    }
}