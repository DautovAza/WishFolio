using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories;

namespace WishFolio.Application.UseCases.Wishlists;

public class WishListItemReservationService : IWishListItemReservationService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserRepository _userRepository;
    private readonly IWishListRepository _wishListRepository;
    private readonly IUnitOfWork _unitOfWork;

    public WishListItemReservationService(ICurrentUserService currentUserService,
        IUserRepository userRepository,
        IWishListRepository wishListRepository,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _userRepository = userRepository;
        _wishListRepository = wishListRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ReserveItem(Guid itemId, bool isAnonymous)
    {
        var currentUser = await _currentUserService.GetCurrentUserAsync();
        var item = await _wishListRepository.GetWishListItemByIdAsync(itemId);

        if (item is null)
        {
            throw new KeyNotFoundException($"Предмет виш-листа с идентефикатором {itemId} не найден!");
        }

        item.ReserveItem(currentUser, isAnonymous);
        await _unitOfWork.SaveChangesAsync();
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
        await _unitOfWork.SaveChangesAsync();
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
        await _unitOfWork.SaveChangesAsync();
    }
}