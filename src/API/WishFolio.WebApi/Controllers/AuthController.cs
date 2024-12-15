using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using WishFolio.WebApi.Controllers.Dtos;
using WishFolio.Application.Services.Accounts;

namespace WishFolio.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AuthController(IAccountService accountService)
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
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        await _accountService.RegisterAsync(request.Email, request.Name, request.Age, request.Password);
        return Ok(new { message = "Регистрация успешна." });
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
        var token = await _accountService.LoginAsync(request.Email, request.Password);
        return Ok(new { token });
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
