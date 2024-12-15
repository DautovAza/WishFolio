using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WishFolio.Application.Services.Friends;

namespace WishFolio.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FriendsController : ControllerBase
    {
        private readonly IFriendService _friendService;

        public FriendsController(IFriendService friendService)
        {
            _friendService = friendService;
        }

        /// <summary>
        /// Добавить друга
        /// </summary>
        /// <param name="friendId">ID пользователя для добавления в друзья</param>
        [HttpPost("{friendId}")]
        public async Task<IActionResult> AddFriend(Guid friendId)
        {
            await _friendService.AddFriendAsync(friendId);
            return Ok(new { Message = "Запрос на добавление в друзья отправлен." });
        }

        /// <summary>
        /// Удалить друга
        /// </summary>
        /// <param name="friendId">ID пользователя для удаления из друзей</param>
        [HttpDelete("{friendId}")]
        public async Task<IActionResult> RemoveFriend(Guid friendId)
        {
            await _friendService.RemoveFriendAsync(friendId);
            return NoContent();
        }
    }

}
