using WishFolio.Application.Common;

namespace WishFolio.Application.UseCases.UserProfile.Commands.UpdateProfile;

public record UpdateProfileCommand : RequestBase
{
    public string? Name { get; set; }
    public int Age { get; set; }
}
