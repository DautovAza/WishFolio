using AutoMapper;
using WishFolio.Application.Common;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Shared.ResultPattern;
using WishFolio.Application.UseCases.Friends.Queries.Dtos;
using WishFolio.Domain.Abstractions.Repositories.Read;
using WishFolio.Domain.Abstractions.Entities;

namespace WishFolio.Application.UseCases.Friends.Commands.GetFriends;

public class GetFriendsQueryHandler : RequestHandlerBase<GetFriendsQuery, PagedCollection<FriendDto>>
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

    public override async Task<Result<PagedCollection<FriendDto>>> Handle(GetFriendsQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetCurrentUserId();

        var friends = await _friendsReadRepository.GetUserFrindsAsync(userId, request.FriendshipStatus, request.FilteringInfo, request.PageInfo);
        var friendsDto = _mapper.Map<PagedCollection<FriendDto>>(friends);

        return Ok(friendsDto);
    }
}
