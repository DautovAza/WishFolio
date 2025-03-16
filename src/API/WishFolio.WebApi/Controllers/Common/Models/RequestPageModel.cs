using Microsoft.AspNetCore.Mvc;

namespace WishFolio.WebApi.Controllers.Common.Models;

public class RequestPageModel
{
    [FromQuery]
    public int PageNumber { get; set; } = 1;

    [FromQuery]
    public int PageSize { get; set; } = 5;
}