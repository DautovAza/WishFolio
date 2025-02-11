using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using AutoMapper;
using WishFolio.Domain.Entities.UserAgregate.Friends;
using WishFolio.Application.UseCases.Friends.Queries.Dtos;
using WishFolio.Application.UseCases.Friends.Commands.DeleteFriend;
using WishFolio.Application.UseCases.Friends.Commands.GetFriends;
using WishFolio.WebApi.Controllers.Abstractions;
using WishFolio.WebApi.Controllers.Friends.ViewModels;

namespace WishFolio.WebApi.Controllers.Friends;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FriendsController : MappingResultHandlerControllerBase
{

    public FriendsController(ISender sender, IMapper mapper)
        : base(sender, mapper)
    { }

    [HttpGet]
    public Task<ActionResult<IEnumerable<FriendModel>>> GetFriends()
    {
        return HandleRequestResult<IEnumerable<FriendDto>, IEnumerable<FriendModel>>(new GetFriendsQuery(FriendshipStatus.Accepted));
    }

    [HttpDelete("{friendId}")]
    public Task<ActionResult> RemoveFriend(Guid friendId)
    {
        return HandleRequestResult(new RemoveFriendCommand() { FriendId = friendId });
    }
}
