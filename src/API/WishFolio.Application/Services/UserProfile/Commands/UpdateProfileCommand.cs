using MediatR;

namespace WishFolio.Application.Services.UserProfile.Commands;

public class UpdateProfileCommand : IRequest
{
    public string Name { get; set; }
    public int Age { get; set; }
}
