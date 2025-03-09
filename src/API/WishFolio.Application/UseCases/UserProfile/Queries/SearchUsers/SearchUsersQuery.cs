using WishFolio.Application.Common;
using WishFolio.Application.UseCases.UserProfile.Queries.GetProfile.Dtos;

namespace WishFolio.Application.UseCases.UserProfile.Queries.SearchUsers;

public record SearchUsersQuery(string? FilteringString, string? OrderBy, int PageNumber, int PageSize) : PagedRequestBase<GetProfileDto>(PageNumber, PageSize);
