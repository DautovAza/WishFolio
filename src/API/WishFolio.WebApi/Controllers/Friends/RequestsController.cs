using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using WishFolio.WebApi.Controllers.Friends.ViewModels;
using WishFolio.Application.UseCases.Friends.Queries.Dtos;
using WishFolio.Application.UseCases.Friends.Commands.GetFriends;
using WishFolio.Application.UseCases.Friends.Commands.CreateFriendRequest;
using WishFolio.Application.UseCases.Friends.Commands.AcceptFriendRequest;
using WishFolio.Application.UseCases.Friends.Commands.RejectFriendRequest;
using WishFolio.Domain.Entities.UserAgregate.Friends;
using WishFolio.WebApi.Controllers.Abstractions;

namespace WishFolio.WebApi.Controllers.Friends;

[Authorize]
[ApiController]
[Route("api/friends/[controller]")]
public class RequestsController : ResultHandlerControllerBase
{
    public RequestsController(ISender sender) : base(sender)
    {
    }

    [HttpPost()]
    public async Task<ActionResult> AddFriend([FromBody] AddFriendRequest request)
    {
        if (request.FriendId.HasValue)
        {
            return await HandleResultResponseForRequest(new CreateFriendRequestByIdCommand(request.FriendId.Value));
        }
        else
        {
            return await HandleResultResponseForRequest(new CreateFriendRequestByEmailCommand(request.FriendEmail));
        }
    }

    [HttpPut("{friendId}/accept")]
    public async Task<ActionResult> AcceptRequest(Guid friendId)
    {
        return await HandleResultResponseForRequest(new AcceptFriendRequestCommand(friendId));
    }

    [HttpPut("{friendId}/reject")]
    public async Task<ActionResult> RejectRequest(Guid friendId)
    {
        return await HandleResultResponseForRequest(new RejectFriendRequestCommand(friendId));
    }

    [HttpGet("incoming")]
    public async Task<ActionResult<List<FriendDto>>> GetIncomingRequests()
    {
        return await HandleResultResponseForRequest(new GetFriendsQuery(FriendshipStatus.Requested));
    }

    [HttpGet("sent")]
    public async Task<ActionResult<List<FriendDto>>> GetSentRequests()
    {
        return await HandleResultResponseForRequest(new GetFriendsQuery(FriendshipStatus.Pending));
    }
}
