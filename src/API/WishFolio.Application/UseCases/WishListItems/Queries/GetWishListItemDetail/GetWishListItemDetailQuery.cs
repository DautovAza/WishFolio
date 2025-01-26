using AutoMapper;
using WishFolio.Application.Common;
using WishFolio.Application.UseCases.Wishlists.Queries.Dtos;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Application.UseCases.WishListItems.Queries.GetWishListItemDetail;

public record GetWishListItemDetailQuery(Guid UserId, string WishListName, Guid ItemId)
    : RequestBase<WishListItemDetailsDto>;

public class GetWishListItemDetailQueryHandler : RequestHandlerBase<GetWishListItemDetailQuery, WishListItemDetailsDto>
{
    private readonly IWishListRepository _wishListRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetWishListItemDetailQueryHandler(IWishListRepository wishListRepository,
        ICurrentUserService currentUserService,
        IUserRepository userRepository,
        IMapper mapper)
    {
        _wishListRepository = wishListRepository;
        _currentUserService = currentUserService;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public override async Task<Result<WishListItemDetailsDto>> Handle(GetWishListItemDetailQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.GetCurrentUserId();
        var user = await _userRepository.GetByIdAsync(request.UserId);
        var visabilityLevel = user.GetWihListVisabilityLevelForUser(currentUserId);

        var wishlist = await _wishListRepository.GetOwnerWishListByNameAsync(currentUserId, request.WishListName, visabilityLevel);

        if (wishlist is null)
        {
            return Failure(DomainErrors.WishList.NotFoundByName(request.WishListName));
        }

        var item = wishlist.Items.FirstOrDefault(i => i.Id == request.ItemId);

        if (item is null)
        {
            return Failure(DomainErrors.WishListItem.ItemNotFound(request.ItemId));
        }

        return Ok(_mapper.Map<WishListItemDetailsDto>(item));
    }
}
