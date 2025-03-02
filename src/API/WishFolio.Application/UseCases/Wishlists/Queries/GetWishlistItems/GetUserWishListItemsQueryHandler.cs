using AutoMapper;
using WishFolio.Application.Common;
using WishFolio.Application.UseCases.Wishlists.Queries.Dtos;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Shared.ResultPattern;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories.Read;
using WishFolio.Domain.Abstractions.Repositories.Write;
using WishFolio.Domain.Abstractions.Entities;

namespace WishFolio.Application.UseCases.Wishlists.Queries.GetWishlistItems;

public class GetUserWishListItemsQueryHandler : RequestHandlerBase<GetWishlistItemsQuery, PagedCollection<WishListItemDto>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserRepository _userRepository;
    private readonly IWishlistReadRepository _wishListRepository;
    private readonly IMapper _mapper;

    public GetUserWishListItemsQueryHandler(ICurrentUserService currentUserService,
        IUserRepository userRepository,
        IWishlistReadRepository wishListRepository,
        IMapper mapper)
    {
        _currentUserService = currentUserService;
        _userRepository = userRepository;
        _wishListRepository = wishListRepository;
        _mapper = mapper;
    }

    public override async Task<Result<PagedCollection<WishListItemDto>>> Handle(GetWishlistItemsQuery request, CancellationToken cancellationToken)
    {
        var watchUserId = _currentUserService.GetCurrentUserId();
        var ownerUser = await _userRepository.GetByIdAsync(request.UserId);

        if (ownerUser is null)
        {
            return Failure(DomainErrors.User.UserNotFound());
        }

        var visabilityLevel = ownerUser.GetWihListVisabilityLevelForUser(watchUserId);
        var wishlist = await _wishListRepository.GetUserWishlistsByNameAsync(ownerUser.Id,request.WishListName, visabilityLevel);
        
        if (wishlist is null)
        {
            return Failure(DomainErrors.WishList.NotFoundByName(request.WishListName));
        }

        var pagedItems = await _wishListRepository.GetPagedWishlistItemsAsync(wishlist.Id,request.PageNumber, request.PageSize);

        return Ok(_mapper.Map<PagedCollection<WishListItemDto>>(pagedItems));
    }
}
