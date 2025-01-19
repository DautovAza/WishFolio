using Microsoft.EntityFrameworkCore;
using WishFolio.Domain.Abstractions.Repositories;
using WishFolio.Domain.Entities.UserAgregate.Profile;
using WishFolio.Domain.Entities.UserAgregate;

namespace WishFolio.Infrastructure.Dal.Repositories;

public class UserRepository : IUserRepository
{
    private readonly WishFolioContext _context;

    public UserRepository(WishFolioContext context)
    {
        _context = context;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        if (email == null)
        {
            throw new ArgumentNullException(nameof(email));
        }

        return await _context.Users
            .Include(u => u.Profile)
            .Include(u => u.Friends)
            .Include(u => u.Notifications)
            .FirstOrDefaultAsync(u => u.Email.Address == email);
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(u => u.Profile)
            .Include(u => u.Friends)
            .Include(u => u.Notifications)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task AddAsync(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        await _context.Users.AddAsync(user);
    }

    public async Task<UserProfile> GetProfileByIdAsync(Guid id)
    {
        return (await _context.Users
             .Include(u => u.Profile)
             .FirstOrDefaultAsync(u => u.Id == id))?
             .Profile;
    }
}
