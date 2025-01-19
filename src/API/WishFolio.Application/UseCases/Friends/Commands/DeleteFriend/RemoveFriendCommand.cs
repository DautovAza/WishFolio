using WishFolio.Application.Common;

namespace WishFolio.Application.UseCases.Friends.Commands.DeleteFriend;

public record RemoveFriendCommand : RequestBase
{
    public Guid FriendId { get; set; }
}
