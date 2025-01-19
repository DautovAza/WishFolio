using AutoMapper;
using WishFolio.Application.UseCases.Wishlists.Dtos;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories;
using WishFolio.Domain.Entities.WishListAgregate;

namespace WishFolio.Application.UseCases.Wishlists;

public class WishListsService : IWishListsService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserRepository _userRepository;
    private readonly IWishListRepository _wishListRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public WishListsService(ICurrentUserService currentUserService,
        IUserRepository userRepository,
        IWishListRepository wishListRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _userRepository = userRepository;
        _wishListRepository = wishListRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<WishListDto>> GetAllUserWishListsAsync(Guid userId)
    {
        var currentUserId = _currentUserService.GetCurrentUserId();
        var user = await _userRepository.GetByIdAsync(userId);

        var visabilityLevel = user.GetWihListVisabilityLevelForUser(userId);
        var wishlists = await _wishListRepository.GetOwnerWishListsAsync(userId, visabilityLevel);

        return _mapper.Map<List<WishListDto>>(wishlists);
    }

    public async Task AddWishListAsync(string name, string description, VisabilityLevel visabilityLevel)
    {
        var currentUserId = _currentUserService.GetCurrentUserId();
        var wishList = new WishList(currentUserId, name, description, visabilityLevel, await _wishListRepository.IsUniqWishListNameForUser(name, currentUserId));

        await _wishListRepository.AddAsync(wishList);
        await _unitOfWork.SaveChangesAsync();
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
        await _unitOfWork.SaveChangesAsync();
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
        await _unitOfWork.SaveChangesAsync();
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

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveWishListItemAsync(string wishListName, Guid itemId)
    {
        var currentUserId = _currentUserService.GetCurrentUserId();
        var wishlist = await _wishListRepository.GetOwnerWishListByNameAsync(currentUserId, wishListName, VisabilityLevel.Private);

        wishlist.RemoveItem(itemId);

        await _unitOfWork.SaveChangesAsync();
    }
}
