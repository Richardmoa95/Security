using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserDataService.Application.DTOs;
using UserDataService.Application.Interfaces;

namespace UserDataService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserById(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        var tokenEmail = User.FindFirst(ClaimTypes.Email)?.Value;

        if (!string.Equals(user.Email, tokenEmail, StringComparison.OrdinalIgnoreCase))
        {
            return Forbid();
        }

        return Ok(user);
    }
}
