using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using WishFolio.Domain.Entities.UserAgregate.Friends;
using WishFolio.Application.UseCases.Friends.Queries.Dtos;
using WishFolio.Application.UseCases.Friends.Commands.DeleteFriend;
using WishFolio.Application.UseCases.Friends.Commands.GetFriends;
using WishFolio.WebApi.Controllers.Abstractions;

namespace WishFolio.WebApi.Controllers.Friends;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FriendsController : ResultHandlerControllerBase
{

    public FriendsController(ISender sender)
        : base(sender)
    { }

    [HttpGet]
    public async Task<ActionResult<List<FriendDto>>> GetFriends()
    {
        return await HandleResultResponseForRequest(new GetFriendsQuery(FriendshipStatus.Accepted));
    }

    [HttpDelete("{friendId}")]
    public async Task<ActionResult> RemoveFriend(Guid friendId)
    {
        return await HandleResultResponseForRequest(new RemoveFriendCommand() { FriendId = friendId });
    }
}
