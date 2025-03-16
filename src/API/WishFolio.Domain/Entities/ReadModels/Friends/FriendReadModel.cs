using WishFolio.Domain.Entities.UserAgregate.Friends;

namespace WishFolio.Domain.Entities.ReadModels.Friends;

public class FriendReadModel
{
    public Guid Id { get; set; }
    public FriendshipStatus Status { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
