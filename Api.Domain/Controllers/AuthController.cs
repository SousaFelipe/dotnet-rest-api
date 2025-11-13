using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Domain.Helpers;
using Api.Service.Dtos;
using Api.Service.Interfaces;


namespace Api.Domain.Controllers;


[Route("auth")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    private IAuthService AuthService { get; init; } = authService;


    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] AuthRequestDto authDto)
    {
        var auth = await AuthService.TryToLogin(authDto);
        return Ok(auth);
    }
}
