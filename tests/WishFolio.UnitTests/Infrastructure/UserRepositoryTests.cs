using Microsoft.EntityFrameworkCore;
using Moq;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Entities.UserAgregate.ValueObjects;
using WishFolio.Infrastructure.Dal.Write;
using WishFolio.Infrastructure.Dal.Write.Repositories;

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

        Email email = Email.Create("test@example.com").Value;
        User user = User.Create(email.Address, "Test User", 25, "Password", _passwordHasherMoq.Object);


        await repository.AddAsync(user);
        await context.SaveChangesAsync();

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

        Email email1 = Email.Create("user1@example.com");
        User user1 = User.Create(email1.Address, "User One", 30, "Password", _passwordHasherMoq.Object).Value;

        Email email2 = Email.Create("user2@example.com");
        User user2 = User.Create(email2.Address, "User Two", 28,"Password", _passwordHasherMoq.Object);

        await repository.AddAsync(user1);
        await repository.AddAsync(user2);
        await context.SaveChangesAsync();

        var retrievedUser = await repository.GetByEmailAsync(Email.Create("user2@example.com").Value.Address);
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