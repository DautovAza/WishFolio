using WishFolio.Application.Common;
using WishFolio.Domain.Errors;
using WishFolio.Domain.Abstractions.Auth;
using WishFolio.Domain.Abstractions.Repositories;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.Application.UseCases.Friends.Commands.CreateFriendRequest;

public class CreateFriendRequestByIdCommandHandler : RequestHandlerBase<CreateFriendRequestByIdCommand> 
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateFriendRequestByIdCommandHandler(ICurrentUserService currentUserService, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public override async Task<Result> Handle(CreateFriendRequestByIdCommand request, CancellationToken cancellationToken)
    {
        var user = await _currentUserService.GetCurrentUserAsync();
        var friend = await _userRepository.GetByIdAsync(request.Id);

        if (user == null || friend == null)
        {
            return Failure(DomainErrors.User.UserNotFound());
        }

        if (user.Id == friend.Id)
        {
            return Failure(DomainErrors.Friend.CannotAddSelfToFriends());
        }

        var result = user.AddToFriends(friend);

        if (result.IsSuccess)
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return result;
    }
}
