using AutoMapper;
using WishFolio.Application.Common;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Entities.UserAgregate.Friends;
using WishFolio.Domain.Shared.ResultPattern;
using WishFolio.Application.UseCases.Friends.Queries.Dtos;

namespace WishFolio.Application.UseCases.Friends.Commands.GetFriends;

public class GetFriendsQueryHandler : RequestHandlerBase<GetFriendsQuery, IEnumerable<FriendDto>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetFriendsQueryHandler(ICurrentUserService currentUserService, IMapper mapper)
    {
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public override async Task<Result<IEnumerable<FriendDto>>> Handle(GetFriendsQuery request, CancellationToken cancellationToken)
    {
        var user = await _currentUserService.GetCurrentUserAsync();

        if (user == null)
        {
            return Result<IEnumerable<FriendDto>>.Failure(DomainErrors.User.UserNotFound());
        }

        var friends = user.Friends
            .Where(f => f.Status == request.FriendshipStatus)
            .ToList();

        var friendsDto = friends
            .Select(_mapper.Map<Friendship, FriendDto>)
            .ToList();

        return Ok(friendsDto);
    }
}
