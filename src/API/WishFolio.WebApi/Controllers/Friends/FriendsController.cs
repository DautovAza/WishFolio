using Microsoft.AspNetCore.Mvc;
using WishFolio.Application.Services.Friends.Dtos;
using WishFolio.Application.Services.Friends;
using Microsoft.AspNetCore.Authorization;

namespace WishFolio.WebApi.Controllers.Friends;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FriendsController : ControllerBase
{
    private readonly IFriendService _friendService;

    public FriendsController(IFriendService friendService)
    {
        _friendService = friendService;
    }

    [HttpPost("{friendId}")]
    public async Task<IActionResult> AddFriend(Guid friendId)
    {
        await _friendService.AddFriendAsync(friendId);
        return Ok();
    }

    [HttpDelete("{friendId}")]
    public async Task<IActionResult> RemoveFriend(Guid friendId)
    {
        await _friendService.RemoveFriendAsync(friendId);
        return NoContent();
    }

    [HttpGet]
    public async Task<IEnumerable<FriendDto>> GetFriends()
    {
        return await _friendService.GetFriendsAsync();
    }
}
