using WishFolio.Application.Common;
using WishFolio.Application.UseCases.Friends.Queries.Dtos;
using WishFolio.Domain.Abstractions.Entities;
using WishFolio.Domain.Entities.UserAgregate.Friends;

namespace WishFolio.Application.UseCases.Friends.Commands.GetFriends;

public record GetFriendsQuery(FriendshipStatus FriendshipStatus, FilteringInfo FilteringInfo, PageInfo PageInfo) : RequestBase<PagedCollection<FriendDto>> { }
