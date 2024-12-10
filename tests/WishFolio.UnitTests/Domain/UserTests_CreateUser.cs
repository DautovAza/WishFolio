using System.ComponentModel.DataAnnotations;
using WishFolio.Domain.Entities.UserAgregate;
using WishFolio.Domain.Entities.UserAgregate.Profile;

namespace WishFolio.UnitTests.Domain;

public class UserTests_CreateUser
{
    [Theory]
    [InlineData(["test@example.com", "Tester", 45])]
    [InlineData(["test@gmail.com", "Io", UserProfileInvariants.MinAge])]
    [InlineData(["test@gmail.com", "Tester", UserProfileInvariants.MaxAge])]
    public void CreateUser_WithValidData_ShouldSeccesed(string email, string name, int age)
    {
        var user = new User(email, name, age);

        Assert.NotEqual(Guid.Empty, user.Id);
        Assert.Equal(email, user.Email.Address);
        Assert.Equal(name, user.Profile.Name);
    }

    [Theory]

    [InlineData([null])]
    [InlineData("example.com")]
    [InlineData("example@gmail")]
    [InlineData(["test-example.com"])]
    public void CreateUser_WithInalidEmail_ShouldThrow(string email)
    {
        Assert.Throws<ArgumentException>(() => new User(email, "Tester", 45));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(["A"])]
    public void CreateUser_WithInalidName_ShouldThrow(string name)
    {
        IList<ValidationResult> validations = TestValidationUtils.ValidateModel(new User("test@example.com", name, 45).Profile);
        Assert.Contains(validations, v => v.MemberNames.Contains(nameof(UserProfile.Name)));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(1)]
    [InlineData(UserProfileInvariants.MinAge - 1)]
    [InlineData(UserProfileInvariants.MaxAge + 1)]
    public void CreateUser_WithInalidAge_ShouldThrow(int age)
    {
        IList<ValidationResult> validations = TestValidationUtils.ValidateModel(new User("test@example.com", "Test", age).Profile);
        Assert.Contains(validations, v => v.MemberNames.Contains(nameof(UserProfile.Age)));
    }
}
