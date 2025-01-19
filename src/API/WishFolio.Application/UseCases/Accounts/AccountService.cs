using WishFolio.Domain.Errors;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Application.UseCases.Accounts;

public class AccountService : IAccountService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(IUserRepository userRepository, ITokenService tokenService, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> RegisterAsync(string email, string name, int age, string password)
    {
        var existingUser = await _userRepository.GetByEmailAsync(email);
        if (existingUser != null)
        {
            return Result.Failure(DomainErrors.User.UserWithSameEmailAlreadyExisted());
        }

        var userResult = User.Create(email, name, age, password, _passwordHasher);

        if (userResult.IsSuccess)
        {
            await _userRepository.AddAsync(userResult);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok();
        }
        return userResult;
    }

    public async Task<Result<string>> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null || !user.ValidatePassword(password, _passwordHasher))
        {
            return Result<string>.Failure(DomainErrors.User.InvalidAuthorization());
        }

        var token = _tokenService.GenerateToken(user);
        return Result<string>.Ok(token);
    }

    public async Task<Result> LogoutAsync(string token)
    {
        //TODO: добавить логику сброса токена
        await Task.CompletedTask;
        return Result.Ok();
    }
}
