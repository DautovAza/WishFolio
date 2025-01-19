using Moq;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Entities.UserAgregate.Profile;
using WishFolio.Domain.Entities.UserAgregate.ValueObjects;
using WishFolio.Domain.Errors;

namespace WishFolio.UnitTests.Domain;

public class UserTests_CreateUser
{
    private readonly Mock<IPasswordHasher> _passwordHasherMoq;

    public UserTests_CreateUser()
    {
        _passwordHasherMoq = new Mock<IPasswordHasher>();
    }

    [Theory]
    [InlineData(["test@example.com", "Tester", 45])]
    [InlineData(["test@gmail.com", "Io", UserProfileInvariants.MinAge])]
    [InlineData(["test@gmail.com", "Tester", UserProfileInvariants.MaxAge])]
    public void CreateUser_WithValidData_ShouldSeccesed(string email, string name, int age)
    {
        User user = User.Create(email, name, age, "Password", _passwordHasherMoq.Object);

        Assert.NotEqual(Guid.Empty, user.Id);
        Assert.Equal(email, user.Email.Address);
        Assert.Equal(name, user.Profile.Name);
    }

    [Fact]
    public void CreateEmail_WithNullAddress_ShouldThrow()
    {
        var result = Email.Create(null);
        Assert.Contains(DomainErrors.User.EmailIsNull(), result.Errors);
    }

    [InlineData("example.com")]
    [InlineData("example@gmail")]
    [InlineData(["test-example.com"])]
    public void CreateEmail_WithInalidFormat_ShouldThrow(string email)
    {
        var result = Email.Create(email);
        Assert.Contains(DomainErrors.User.EmailInvalidFormat(email), result.Errors);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void CreateUserProfile_WithNullOrEmptyName_ShouldThrow(string name)
    {
        var result = UserProfile.Create(name, 45);
       
        Assert.Contains(DomainErrors.User.UserProfileNameIsNullOrEmpty(),result.Errors);
    }

    [Fact]
    public void CreateUserProfile_WithInvalidShortName_ShouldThrow()
    {
        var name = "A";
        var result = UserProfile.Create(name, 45);
       
        Assert.Contains(DomainErrors.User.UserProfileNameLenghtOutOfRange(name),result.Errors);
    }


    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(1)]
    [InlineData(UserProfileInvariants.MinAge - 1)]
    [InlineData(UserProfileInvariants.MaxAge + 1)]
    public void CreateUser_WithInalidAge_ShouldThrow(int age)
    {
        var result = UserProfile.Create("Test", age);

        Assert.Contains(DomainErrors.User.UserProfileInvalidAge(age), result.Errors);
    }
}
