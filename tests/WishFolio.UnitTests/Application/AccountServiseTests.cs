using Moq;
using WishFolio.Application.Services.Accounts;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Entities.UserAgregate.ValueObjects;

namespace WishFolio.UnitTests.Application;

public class AccountServiseTests
{

    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMoq;
    private readonly AccountService _accountService;

    public AccountServiseTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _tokenServiceMock = new Mock<ITokenService>();
        _passwordHasherMoq = new Mock<IPasswordHasher>();
        _accountService = new AccountService(_userRepositoryMock.Object, _tokenServiceMock.Object, _passwordHasherMoq.Object);

        _passwordHasherMoq.Setup(f => f.VerifyPassword(It.IsAny<string>(),"Password")).Returns(true);
        _passwordHasherMoq.Setup(f => f.VerifyPassword(It.IsAny<string>(), "notValidPassword")).Returns(false);
        _passwordHasherMoq.Setup(f => f.HashPassword(It.IsAny<string>())).Returns("Password");

    }

    [Fact]
    public async Task RegisterAsync_ShouldCreateUser_WhenEmailIsUnique()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<Email>()))
            .ReturnsAsync((User)null);

        // Act
        await _accountService.RegisterAsync("test@example.com", "Test User", 45, "Password");

        // Assert
        _userRepositoryMock.Verify(repo => repo.AddAsync(It.Is<User>(u => u.Email.Address== "test@example.com")), Times.Once);
        _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task RegisterAsync_ShouldThrowException_WhenEmailExists()
    {
        // Arrange
        var existingUser = new User("test@example.com", "Existing User",  25);
        existingUser.SetPassword("Password", _passwordHasherMoq.Object);

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<Email>()))
            .ReturnsAsync(existingUser);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _accountService.RegisterAsync("test@example.com", "Test User", 45, "Password"));
    }

    [Fact]
    public async Task LoginAsync_ShouldReturnToken_WhenCredentialsAreValid()
    {
        // Arrange
        var user = new User("test@example.com", "Test User",  45 );
        user.SetPassword("Password", _passwordHasherMoq.Object);

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<Email>()))
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
        var user = new User("test@example.com", "Test User", 45);
        user.SetPassword("Password", _passwordHasherMoq.Object);

        _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<Email>()))
            .ReturnsAsync(user);

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            _accountService.LoginAsync("test@example.com", "notValidPassword"));
    }

}