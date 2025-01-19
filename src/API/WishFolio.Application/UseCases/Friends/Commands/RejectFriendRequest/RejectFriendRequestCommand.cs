using WishFolio.Application.Common;

namespace WishFolio.Application.UseCases.Friends.Commands.RejectFriendRequest;

public record RejectFriendRequestCommand(Guid FriendId) : RequestBase { }
