using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using WishFolio.Application.UseCases.Accounts;
using WishFolio.WebApi.Controllers.Authorization.ViewModels;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.WebApi.Controllers.Authorization;

[ApiController]
[Route("api/[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AuthorizationController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    /// <summary>
    /// Регистрация нового пользователя.
    /// </summary>
    /// <param name="request">Данные регистрации.</param>
    /// <returns>Статус регистрации.</returns>
    [HttpPost("register")]
    [SwaggerOperation(Summary = "Регистрация нового пользователя")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _accountService.RegisterAsync(request.Email, request.Name, request.Age, request.Password);
        if (result.IsFailure)
        {
            return BadRequest(result.Errors.First().Message);
        }

        return Ok();
    }

    /// <summary>
    /// Вход пользователя.
    /// </summary>
    /// <param name="request">Данные для входа.</param>
    /// <returns>JWT токен.</returns>
    [HttpPost("login")]
    [SwaggerOperation(Summary = "Вход пользователя")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var tokenResult = await _accountService.LoginAsync(request.Email, request.Password);

        if (tokenResult.IsFailure)
        {
            return BadRequest(tokenResult.Errors.First().Message);
        }

        return Ok(new { tokenResult.Value });
    }

    /// <summary>
    /// Выход пользователя.
    /// </summary>
    /// <returns>Статус выхода.</returns>
    [HttpPost("logout")]
    [Authorize]
    [SwaggerOperation(Summary = "Выход пользователя")]
    public async Task<IActionResult> Logout()
    {
        var token = Request.Headers["Authorization"].ToString()
            .Replace("Bearer ", "");

        await _accountService.LogoutAsync(token);
        return Ok(new { message = "Выход успешен." });
    }
}
