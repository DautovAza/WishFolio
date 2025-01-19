using Moq;
using WishFolio.Application.UseCases.Accounts;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Errors;

namespace WishFolio.UnitTests.Application;

public class AccountServiceTests
{

    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMoq;
    private readonly Mock<IUnitOfWork> _unitOfWorkMoq;
    private readonly AccountService _accountService;

    public AccountServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _tokenServiceMock = new Mock<ITokenService>();
        _passwordHasherMoq = new Mock<IPasswordHasher>();
        _unitOfWorkMoq = new Mock<IUnitOfWork>();
        _accountService = new AccountService(_userRepositoryMock.Object, _tokenServiceMock.Object, _passwordHasherMoq.Object, _unitOfWorkMoq.Object);

        _passwordHasherMoq.Setup(f => f.VerifyPassword(It.IsAny<string>(),"Password")).Returns(true);
        _passwordHasherMoq.Setup(f => f.VerifyPassword(It.IsAny<string>(), "notValidPassword")).Returns(false);
        _passwordHasherMoq.Setup(f => f.HashPassword(It.IsAny<string>())).Returns("Password");

    }

    [Fact]
    public async Task RegisterAsync_ShouldCreateUser_WhenEmailIsUnique()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User)null);

        // Act
        await _accountService.RegisterAsync("test@example.com", "Test User", 45, "Password");

        // Assert
        _userRepositoryMock.Verify(repo => repo.AddAsync(It.Is<User>(u => u.Email.Address== "test@example.com")), Times.Once);
        _unitOfWorkMoq.Verify(uw => uw.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task RegisterAsync_ShouldThrowException_WhenEmailExists()
    {
        // Arrange
        User existingUser =  User.Create ("test@example.com", "Existing User",  25,"Password", _passwordHasherMoq.Object);

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(existingUser);

        // Act
        var result = await _accountService.RegisterAsync("test@example.com", "Test User", 45, "Password");

        // Assert
        Assert.Contains(DomainErrors.User.UserWithSameEmailAlreadyExisted(), result.Errors);
    }

    [Fact]
    public async Task LoginAsync_ShouldReturnToken_WhenCredentialsAreValid()
    {
        // Arrange
        User user = User.Create("test@example.com", "Test User",  45 ,"Password", _passwordHasherMoq.Object);

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        _tokenServiceMock.Setup(ts => ts.GenerateToken(user))
            .Returns("jwt-token");

        // Act
        var token = await _accountService.LoginAsync("test@example.com", "Password");

        // Assert
        Assert.Equal("jwt-token", token);
    }

    [Fact]
    public async Task LoginAsync_ShouldThrowException_WhenCredentialsAreInvalid()
    {
        // Arrange
        User user = User.Create ("test@example.com", "Test User", 45,"Password", _passwordHasherMoq.Object);

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        // Act
        var result = await _accountService.LoginAsync("test@example.com", "notValidPassword");

        // Assert
        Assert.Contains(DomainErrors.User.InvalidAuthorization(), result.Errors);
    }

}