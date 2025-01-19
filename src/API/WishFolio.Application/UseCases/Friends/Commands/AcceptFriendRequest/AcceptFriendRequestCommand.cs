using WishFolio.Application.Common;

namespace WishFolio.Application.UseCases.Friends.Commands.AcceptFriendRequest;

public record AcceptFriendRequestCommand(Guid FriendId) : RequestBase { }
