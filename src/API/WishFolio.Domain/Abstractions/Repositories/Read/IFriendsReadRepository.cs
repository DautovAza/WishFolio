using WishFolio.Domain.Entities.ReadModels.Friends;
using WishFolio.Domain.Entities.UserAgregate.Friends;

namespace WishFolio.Domain.Abstractions.Repositories.Read;

public interface IFriendsReadRepository
{
    Task<IEnumerable<FriendReadModel>> GetUserFriendsAsync(Guid userId, FriendshipStatus friendshipStatus);
}
