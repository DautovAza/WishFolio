using WishFolio.Application.Common;

namespace WishFolio.Application.UseCases.Friends.Commands.CreateFriendRequest;

public record CreateFriendRequestByEmailCommand(string? FriendEmail) : RequestBase { }
