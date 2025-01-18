using MediatR;
using WishFolio.Application.Services.UserProfile.Queries.Dtos;

namespace WishFolio.Application.Services.UserProfile.Queries;

public class GetProfileQuery : IRequest<GetProfileResponse>
{
}
