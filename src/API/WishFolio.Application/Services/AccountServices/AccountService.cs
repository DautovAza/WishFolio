using WishFolio.Domain.Abstractions.Entities;
using WishFolio.Domain.Abstractions.Repositories;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Entities.UserAgregate.ValueObjects;

namespace WishFolio.Application.Services.AccountServices;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;

    public AccountService(IUserRepository userRepository, ITokenService tokenService, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
    }

    public async Task RegisterAsync(string email, string name, int age, string password)
    {
        var existingUser = await _userRepository.GetByEmailAsync(new Email(email));
        if (existingUser != null)
        {
            throw new InvalidOperationException("Пользователь с таким email уже существует.");
        }

        var user = new User(email, name, age);
        user.SetPassword(password, _passwordHasher);

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(new Email(email));
        if (user == null || !user.ValidatePassword(password, _passwordHasher))
        {
            throw new UnauthorizedAccessException("Неверный email или пароль.");
        }

        return _tokenService.GenerateToken(user);
    }

    public Task LogoutAsync(string token)
    {
        //TODO: добавить логику сброса токена
        return Task.CompletedTask;
    }
}
