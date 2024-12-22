using Microsoft.AspNetCore.Mvc;
using WishFolio.Application.Services.Friends.Dtos;
using WishFolio.Application.Services.Friends;
using Microsoft.AspNetCore.Authorization;

namespace WishFolio.WebApi.Controllers.Friends;

[Authorize]
[ApiController]
[Route("api/[controller]-")]
public class FriendRequestsController : ControllerBase
{
    private readonly IFriendService _friendService;

    public FriendRequestsController(IFriendService friendService)
    {
        _friendService = friendService;
    }

    [HttpPost("{friendId}/accept")]
    public async Task<IActionResult> AcceptRequest(Guid friendId)
    {
        await _friendService.AcceptFriendRequest(friendId);
        return Ok();
    }

    [HttpPost("{friendId}/reject")]
    public async Task<IActionResult> RejectRequest(Guid friendId)
    {
        await _friendService.RejectFriendRequest(friendId);
        return Ok();
    }

    [HttpGet("incoming")]
    public async Task<IEnumerable<FriendDto>> GetIncomingRequests()
    {
        return await _friendService.GetIncommingFriendsInvitations();
    }

    [HttpGet("sent")]
    public async Task<IEnumerable<FriendDto>> GetSentRequests()
    {
        return await _friendService.GetSentFriendsInvitations();
    }
}
