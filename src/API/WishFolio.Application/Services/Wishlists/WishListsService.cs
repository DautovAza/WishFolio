using AutoMapper;
using WishFolio.Application.Services.Wishlists.Dtos;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories;
using WishFolio.Domain.Entities.WishListAgregate;

namespace WishFolio.Application.Services.Wishlists;

public class WishListsService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserRepository _userRepository;
    private readonly IWishListRepository _wishListRepository;
    private readonly IMapper _mapper;

    public async Task<List<WishListDto>> GetAllUserWishListsAsync(Guid userId)
    {
        var currentUserId = _currentUserService.GetCurrentUserId();
        var user = await _userRepository.GetByIdAsync(userId);

        var visabilityLevel = user.GetWihListVisabilityLevelForUser(userId);
        var wishlists = await _wishListRepository.GetOwnerWishListsAsync(userId, visabilityLevel);

        return _mapper.Map<List<WishListDto>>(wishlists);
    }

    public async Task<WishListDto?> GetUserWishListByNameAsync(Guid userId, string name)
    {
        var currentUserId = _currentUserService.GetCurrentUserId();
        var user = await _userRepository.GetByIdAsync(userId);

        var visabilityLevel = user.GetWihListVisabilityLevelForUser(userId);
        var wishlist = await _wishListRepository.GetOwnerWishListByNameAsync(userId, name, visabilityLevel);

        return _mapper.Map<WishListDto>(wishlist);
    }

    public async Task AddWishListAsync(string name, string description, VisabilityLevel visabilityLevel)
    {
        var currentUserId = _currentUserService.GetCurrentUserId();
        var wishList = new WishList(currentUserId, name, description, visabilityLevel, await _wishListRepository.IsUniqWishListNameForUser(name, currentUserId));

        await _wishListRepository.AddAsync(wishList);
        await _wishListRepository.SaveChangesAsync();
    }

    public async Task RemoveWishListByNameAsync(string name)
    {
        var currentUserId = _currentUserService.GetCurrentUserId();
        var wishlist = await _wishListRepository.GetOwnerWishListByNameAsync(currentUserId, name, VisabilityLevel.Private);

        if (wishlist is null)
        {
            throw new KeyNotFoundException($"Виш-лист с именем {name} не найден или недоступен!");
        }
        await _wishListRepository.RemoveAsync(wishlist);
        await _wishListRepository.SaveChangesAsync();
    }

    public async Task<WishListItemDetailsDto> GetWishListItemDetailAsync(Guid userId, string wishListName, Guid itemId)
    {
        var currentUserId = _currentUserService.GetCurrentUserId();
        var user = await _userRepository.GetByIdAsync(userId);

        var visabilityLevel = user.GetWihListVisabilityLevelForUser(userId);
        var wishlist = await _wishListRepository.GetOwnerWishListByNameAsync(userId, wishListName, visabilityLevel);

        if (wishlist is null)
        {
            throw new KeyNotFoundException($"Виш-лист с именем {wishListName} не найден или недоступен!");
        }

        var item = wishlist.Items.FirstOrDefault(i => i.Id == itemId);

        if (item is null)
        {
            throw new KeyNotFoundException($"Предмет виш-листа с идентефикатором {itemId} не найден!");
        }

        return _mapper.Map<WishListItemDetailsDto>(item);
    }

    public async Task AddWishListItemAsync(string wishListName, string name, string description, string linkUrl)
    {
        var item = new WishlistItem(name, description, linkUrl);
        var currentUserId = _currentUserService.GetCurrentUserId();
        var wishlist = await _wishListRepository.GetOwnerWishListByNameAsync(currentUserId, wishListName, VisabilityLevel.Private);
        wishlist.AddItem(item);
        await _wishListRepository.SaveChangesAsync();
    }

    public async Task UpdateWishListItemAsync(string wishListName, Guid itemId, string name, string description, string linkUrl)
    {
        var currentUserId = _currentUserService.GetCurrentUserId();
        var wishlist = await _wishListRepository.GetOwnerWishListByNameAsync(currentUserId, wishListName, VisabilityLevel.Private);
        var item = wishlist.Items.FirstOrDefault(i => i.Id == itemId);

        if (item is null)
        {
            throw new KeyNotFoundException($"Предмет виш-листа с идентефикатором {itemId} не найден!");
        }

        item.Update(name, description, linkUrl);

        await _wishListRepository.SaveChangesAsync();
    }

    public async Task RemoveWishListItemAsync(string wishListName, Guid itemId)
    {
        var currentUserId = _currentUserService.GetCurrentUserId();
        var wishlist = await _wishListRepository.GetOwnerWishListByNameAsync(currentUserId, wishListName, VisabilityLevel.Private);
        
        wishlist.RemoveItem(itemId);

        await _wishListRepository.SaveChangesAsync();
    }
}
