using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Entities.UserAgregate.Profile;

namespace WishFolio.Domain.Abstractions.Repositories.Write;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(string emailAddress);
    Task<User> GetByIdAsync(Guid id);
    Task<UserProfile> GetProfileByIdAsync(Guid id);
    Task AddAsync(User user);
}
