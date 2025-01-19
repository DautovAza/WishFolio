using WishFolio.Application.Common;

namespace WishFolio.Application.UseCases.Friends.Commands.CreateFriendRequest;

public record CreateFriendRequestByIdCommand(Guid Id) : RequestBase { }