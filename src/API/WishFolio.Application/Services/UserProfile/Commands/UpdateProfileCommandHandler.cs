using MediatR;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories;

namespace WishFolio.Application.Services.UserProfile.Commands;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public UpdateProfileCommandHandler(IUserRepository userRepository, ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _currentUserService.GetCurrentUserAsync();

        user.Profile.Update(request.Name, request.Age);
        await _userRepository.SaveChangesAsync();
    }
}
