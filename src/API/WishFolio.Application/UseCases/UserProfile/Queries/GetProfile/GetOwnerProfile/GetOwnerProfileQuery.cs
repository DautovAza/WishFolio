using WishFolio.Application.Common;
using WishFolio.Application.UseCases.UserProfile.Queries.GetProfile.Dtos;

namespace WishFolio.Application.UseCases.UserProfile.Queries.GetProfile.GetOwnerProfile;

public record GetOwnerProfileQuery : RequestBase<GetDetailedProfileDto> { }
