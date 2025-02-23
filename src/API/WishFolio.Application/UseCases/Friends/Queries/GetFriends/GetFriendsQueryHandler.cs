using AutoMapper;
using WishFolio.Application.Common;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Shared.ResultPattern;
using WishFolio.Application.UseCases.Friends.Queries.Dtos;
using WishFolio.Domain.Abstractions.Repositories.Read;

namespace WishFolio.Application.UseCases.Friends.Commands.GetFriends;

public class GetFriendsQueryHandler : RequestHandlerBase<GetFriendsQuery, IEnumerable<FriendDto>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IFriendsReadRepository _friendsReadRepository;
    private readonly IMapper _mapper;

    public GetFriendsQueryHandler(ICurrentUserService currentUserService, IFriendsReadRepository friendsReadRepository, IMapper mapper)
    {
        _currentUserService = currentUserService;
        _friendsReadRepository = friendsReadRepository;
        _mapper = mapper;
    }

    public override async Task<Result<IEnumerable<FriendDto>>> Handle(GetFriendsQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetCurrentUserId();

        var friends = await _friendsReadRepository.GetUserFriendsAsync(userId, request.FriendshipStatus);
        var friendsDto = _mapper.Map<IEnumerable<FriendDto>>(friends);

        return Ok(friendsDto);
    }
}
