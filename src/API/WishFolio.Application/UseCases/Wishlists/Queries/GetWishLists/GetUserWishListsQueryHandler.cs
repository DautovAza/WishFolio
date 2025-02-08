using AutoMapper;
using WishFolio.Application.Common;
using WishFolio.Application.UseCases.Wishlists.Queries.Dtos;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Shared.ResultPattern;
using WishFolio.Domain.Abstractions.Repositories.Write;

namespace WishFolio.Application.UseCases.Wishlists.Queries.GetWishLists;

public class GetUserWishListsQueryHandler : RequestHandlerBase<GetUserWishListsQuery, IEnumerable<WishListDto>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserRepository _userRepository;
    private readonly IWishListRepository _wishListRepository;
    private readonly IMapper _mapper;

    public GetUserWishListsQueryHandler(ICurrentUserService currentUserService, 
        IUserRepository userRepository,
        IWishListRepository wishListRepository,
        IMapper mapper)
    {
        _currentUserService = currentUserService;
        _userRepository = userRepository;
        _wishListRepository = wishListRepository;
        _mapper = mapper;
    }

    public override async Task<Result<IEnumerable<WishListDto>>> Handle(GetUserWishListsQuery request, CancellationToken cancellationToken)
    {
        var watchUserId = _currentUserService.GetCurrentUserId();
        var ownerUser = await _userRepository.GetByIdAsync(request.userId);

        if (ownerUser is null)
        {
            return Failure(DomainErrors.User.UserNotFound());
        }

        var visabilityLevel = ownerUser.GetWihListVisabilityLevelForUser(watchUserId);
        var wishlists = await _wishListRepository.GetOwnerWishListsAsync(ownerUser.Id, visabilityLevel);

        return Ok( _mapper.Map<List<WishListDto>>(wishlists));
    }
}
