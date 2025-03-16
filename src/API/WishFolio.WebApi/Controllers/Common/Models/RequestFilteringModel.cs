using Microsoft.AspNetCore.Mvc;

namespace WishFolio.WebApi.Controllers.Common.Models;

public class RequestFilteringModel
{
    [FromQuery]
    public string? FilterName { get; set; } = null;

    [FromQuery]
    public string? OrderBy { get; set; } = null;
}


