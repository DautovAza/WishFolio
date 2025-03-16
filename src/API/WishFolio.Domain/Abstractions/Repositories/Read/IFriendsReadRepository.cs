using WishFolio.Domain.Abstractions.Entities;
using WishFolio.Domain.Entities.ReadModels.Friends;
using WishFolio.Domain.Entities.UserAgregate.Friends;

namespace WishFolio.Domain.Abstractions.Repositories.Read;

public interface IFriendsReadRepository
{
    Task<PagedCollection<FriendReadModel>> GetUserFrindsAsync(Guid userId, FriendshipStatus friendshipStatus, FilteringInfo filteringInfo, PageInfo pageInfo);
}
