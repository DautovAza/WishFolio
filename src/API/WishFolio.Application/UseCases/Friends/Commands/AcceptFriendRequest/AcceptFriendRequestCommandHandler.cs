using WishFolio.Application.Common;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Application.UseCases.Friends.Commands.AcceptFriendRequest;

public class AcceptFriendRequestCommandHandler : RequestHandlerBase<AcceptFriendRequestCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public AcceptFriendRequestCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public override async Task<Result> Handle(AcceptFriendRequestCommand request, CancellationToken cancellationToken)
    {
        var user = await _currentUserService.GetCurrentUserAsync();
        var friend = await _userRepository.GetByIdAsync(request.FriendId);

        if (user == null || friend == null)
        {
            return Failure(DomainErrors.User.UserNotFound());
        }

        var result = user.AcceptFriendRequest(friend);
        if (result.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        return result;
    }
}
