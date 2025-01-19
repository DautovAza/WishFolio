using WishFolio.Domain.Entities.UserAgregate.Friends;

namespace WishFolio.Application.UseCases.Friends.Queries.Dtos;

public record class FriendDto
{
    public Guid Id { get; set; }
    public FriendshipStatus Status { get; set; }
    public string Name { get; set; }
}
