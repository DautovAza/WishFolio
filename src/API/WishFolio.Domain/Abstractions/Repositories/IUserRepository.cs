using WishFolio.Domain.Entities.UserAgregate.ValueObjects;
using WishFolio.Domain.Entities.UserAgregate;

namespace WishFolio.Domain.Abstractions.Repositories;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(Email email);
    Task<User> GetByIdAsync(Guid id);
    Task AddAsync(User user);
    Task SaveChangesAsync();
}