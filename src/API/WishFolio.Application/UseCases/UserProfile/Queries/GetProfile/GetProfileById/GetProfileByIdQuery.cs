using WishFolio.Application.Common;
using WishFolio.Application.UseCases.UserProfile.Queries.GetProfile.Dtos;
namespace WishFolio.Application.UseCases.UserProfile.Queries.GetProfile.GetProfileById;

public record GetProfileByIdQuery(Guid Id) : RequestBase<GetProfileDto> { }