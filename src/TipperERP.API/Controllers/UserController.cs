using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipperERP.Application.Users;

namespace TipperERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(new { success = true, data = users });
    }

    [HttpGet("{UserId:guid}")]
    public async Task<IActionResult> GetById(Guid userId)
    {
        var user = await _userService.GetByIdAsync(userId);
        if (user == null) return NotFound(new { success = false, message = "User not found" });
        return Ok(new { success = true, data = user });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserCreateDto dto)
    {
        // TODO: read from JWT
        var createdBy = Guid.Empty;
        var userId = await _userService.CreateAsync(dto, createdBy);
        return Ok(new { success = true, message = "User created", data = new { userId } });
    }

    [HttpPut("{UserId:guid}")]
    public async Task<IActionResult> Update(Guid userId, [FromBody] UserUpdateDto dto)
    {
        var updatedBy = Guid.Empty;
        await _userService.UpdateAsync(userId, dto, updatedBy);
        return Ok(new { success = true, message = "User updated" });
    }

    [HttpDelete("{UserId:guid}")]
    public async Task<IActionResult> Delete(Guid userId)
    {
        var deletedBy = Guid.Empty;
        await _userService.DeleteAsync(userId, deletedBy);
        return Ok(new { success = true, message = "User deleted" });
    }
}
