using ItnoaWorq.Domain.Entities.Identity;
using ItnoaWorq.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ItnoaWorq.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IJwtTokenService _jwt;

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtTokenService jwt)
    { _userManager = userManager; _signInManager = signInManager; _jwt = jwt; }

    public record RegisterDto(string Email, string Password, string FullName);
    public record LoginDto(string Email, string Password);

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var user = new AppUser { UserName = dto.Email, Email = dto.Email };
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);
        await _userManager.AddToRoleAsync(user, "Public");

        var (token, exp) = await _jwt.CreateAccessTokenAsync(user);
        return Ok(new { token, exp, userId = user.Id });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null) return Unauthorized();
        var ok = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!ok.Succeeded) return Unauthorized();

        var (token, exp) = await _jwt.CreateAccessTokenAsync(user);
        return Ok(new { token, exp, userId = user.Id });
    }
}
