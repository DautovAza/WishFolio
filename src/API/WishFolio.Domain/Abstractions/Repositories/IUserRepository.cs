using WishFolio.Domain.Entities.UserAgregate.ValueObjects;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Entities.UserAgregate.Profile;

namespace WishFolio.Domain.Abstractions.Repositories;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(Email email);
    Task<User> GetByIdAsync(Guid id);
    Task<UserProfile> GetProfileByIdAsync(Guid id);
    Task AddAsync(User user);
    Task SaveChangesAsync();
}
