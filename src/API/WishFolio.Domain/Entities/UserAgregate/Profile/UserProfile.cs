using System.ComponentModel.DataAnnotations;

namespace WishFolio.Domain.Entities.UserAgregate.Profile;

public class UserProfile
{
    [Required]
    [MinLength(UserProfileInvariants.NameMinLenght)]
    [MaxLength(UserProfileInvariants.NameMaxLenght)]
    public string Name { get; private set; }

    [Required]
    [Range(UserProfileInvariants.MinAge, UserProfileInvariants.MaxAge)]
    public int Age { get; private set; }

    public UserProfile(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public void Update(string name, int age)
    {
        Name = name;
        Age = age;
    }
}