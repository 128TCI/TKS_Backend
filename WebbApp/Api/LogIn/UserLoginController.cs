using Microsoft.AspNetCore.Mvc;
using Services.DTOs.User;
using Services.Interfaces.Authentication;
using static Services.DTOs.User.LoginDTO;

namespace Timekeeping.WebApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserLoginController : ControllerBase
{
    private readonly IAuthService _authService;

    public UserLoginController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto, CancellationToken ct)
    {
        var response = await _authService.LoginAsync(loginDto, ct);
        // Check if account is suspended
        if (await _authService.IsSuspended(loginDto.UserName))
        {
            return StatusCode(403, new { message = "Account is suspended. Please contact administrator." });
        }
        if (response == null)
            return Unauthorized(new { message = "Invalid Username or Password" });

        
            

        // This will now return the token AND all fields like EmpCode, Email, etc.
        return Ok(response);
    }
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutDTO logoutDto, CancellationToken ct)
    {
        var result = await _authService.LogoutAsync(logoutDto.UserId, ct);

        if (!result)
            return NotFound(new { message = "User not found" });

        return Ok(new { message = "Logged out successfully" });
    }
}