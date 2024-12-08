using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IslamicPOS.Core.Services.Auth;

namespace IslamicPOS.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        IAuthService authService,
        ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResult>> Login([FromBody] LoginRequest request)
    {
        try
        {
            var result = await _authService.LoginAsync(request.Username, request.Password);
            if (!result.Success)
                return Unauthorized(result.Message);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login");
            return StatusCode(500, "An error occurred during login");
        }
    }

    [HttpPost("register")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<AuthResult>> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var result = await _authService.RegisterAsync(new UserRegistration
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = request.Role
            });

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration");
            return StatusCode(500, "An error occurred during registration");
        }
    }

    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var success = await _authService.ChangePasswordAsync(
                userId,
                request.CurrentPassword,
                request.NewPassword
            );

            if (!success)
                return BadRequest("Invalid current password");

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error changing password");
            return StatusCode(500, "An error occurred while changing password");
        }
    }

    [HttpGet("permissions")]
    [Authorize]
    public async Task<ActionResult<List<string>>> GetPermissions()
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var permissions = await _authService.GetUserPermissionsAsync(userId);
            return Ok(permissions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting permissions");
            return StatusCode(500, "An error occurred while getting permissions");
        }
    }
}