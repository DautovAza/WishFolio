using System.ComponentModel.DataAnnotations;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Domain.Entities.UserAgregate.Profile;

public class UserProfile
{
    public string Name { get; private set; }
    public int Age { get; private set; }

#pragma warning disable CS8618 
    private UserProfile()
    {
    }
#pragma warning restore CS8618 

    public Result SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return Result.Failure(DomainErrors.User.UserProfileNameIsNullOrEmpty());
        }

        if (name.Length is < UserProfileInvariants.NameMinLenght or > UserProfileInvariants.NameMaxLenght)
        {
            return Result.Failure(DomainErrors.User.UserProfileNameLenghtOutOfRange(name));
        }

        Name = name;

        return Result.Ok();
    }

    public Result SetAge(int age)
    {
        if (age is < UserProfileInvariants.MinAge or > UserProfileInvariants.MaxAge)
        {
            return Result.Failure(DomainErrors.User.UserProfileInvalidAge(age));
        }
        Age = age;
        return Result.Ok();
    }

    public static Result<UserProfile> Create(string name, int age)
    {
        var profile = new UserProfile();
        Result[] setPropsResults =
        [
            profile.SetName(name),
            profile.SetAge(age)
        ];

        if (setPropsResults.Any(sr => sr.IsFailure))
        {
            return Result<UserProfile>.Combine(profile, setPropsResults);
        }

        return Result<UserProfile>.Ok(profile);
    }
}