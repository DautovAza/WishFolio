using WishFolio.Domain.Entities.UserAgregate.Friends;

namespace WishFolio.Application.Services.Friends.Dtos;

public record class FriendDto
{
    public Guid Id { get; set; }
    public FriendshipStatus Status { get; set; }
}