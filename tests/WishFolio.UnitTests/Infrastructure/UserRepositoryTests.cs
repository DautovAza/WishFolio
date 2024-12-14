using Microsoft.EntityFrameworkCore;
using Moq;
using WishFolio.Domain.Abstractions.Entities;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Entities.UserAgregate.ValueObjects;
using WishFolio.Infrastructure.Dal;
using WishFolio.Infrastructure.Dal.Repositories;

namespace WishFolio.UnitTests.Infrastructure;

public class UserRepositoryTests
{
    private readonly Mock<IPasswordHasher> _passwordHasherMoq;

    public UserRepositoryTests()
    {
        _passwordHasherMoq = new Mock<IPasswordHasher>();
        _passwordHasherMoq.Setup(f => f.VerifyPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
        _passwordHasherMoq.Setup(f => f.HashPassword(It.IsAny<string>())).Returns("Password");
    }

    [Fact]
    public async Task AddAsync_AddsUserSuccessfully()
    {
        using var context = GetInMemoryDbContext();
        var repository = new UserRepository(context);

        var email = new Email("test@example.com");
        var user = new User(email.Address, "Test User", 25);
        user.SetPassword("Password", _passwordHasherMoq.Object);

        await repository.AddAsync(user);
        await repository.SaveChangesAsync();

        var retrievedUser = await repository.GetByIdAsync(user.Id);
        Assert.NotNull(retrievedUser);
        Assert.Equal("test@example.com", retrievedUser.Email.Address);
        Assert.Equal("Test User", retrievedUser.Profile.Name);
        Assert.Equal(25, retrievedUser.Profile.Age);
    }

    [Fact]
    public async Task GetByEmailAsync_ReturnsCorrectUser()
    {
        using var context = GetInMemoryDbContext();
        var repository = new UserRepository(context);

        var email1 = new Email("user1@example.com");
        var user1 = new User(email1.Address, "User One", 30);
        user1.SetPassword("Password", _passwordHasherMoq.Object);

        var email2 = new Email("user2@example.com");
        var user2 = new User(email2.Address, "User Two", 28);
        user2.SetPassword("Password", _passwordHasherMoq.Object);

        await repository.AddAsync(user1);
        await repository.AddAsync(user2);
        await repository.SaveChangesAsync();

        var retrievedUser = await repository.GetByEmailAsync(new Email("user2@example.com"));
        Assert.NotNull(retrievedUser);
        Assert.Equal("User Two", retrievedUser.Profile.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenUserDoesNotExist()
    {
        using var context = GetInMemoryDbContext();
        var repository = new UserRepository(context);

        var retrievedUser = await repository.GetByIdAsync(Guid.NewGuid());
        Assert.Null(retrievedUser);
    }

    private WishFolioContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<WishFolioContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new WishFolioContext(options);
    }
}