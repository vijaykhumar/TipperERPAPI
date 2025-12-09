using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipperERP.Application.Action;

namespace TipperERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ActionController : ControllerBase
{
	private readonly IActionService _service;

	public ActionController(IActionService service)
	{
		_service = service;
	}

	[HttpGet("list")]
	public async Task<IActionResult> GetAll()
	{
		var data = await _service.GetAllAsync();
		return Ok(new { success = true, data });
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetById(Guid id)
	{
		var data = await _service.GetByIdAsync(id);
		if (data == null)
			return NotFound(new { success = false, message = "Action not found" });

		return Ok(new { success = true, data });
	}

	[HttpPost]
	public async Task<IActionResult> Create(ActionCreateDto dto)
	{
		var id = await _service.CreateAsync(dto);
		return Ok(new { success = true, id });
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> Update(Guid id, ActionUpdateDto dto)
	{
		await _service.UpdateAsync(id, dto);
		return Ok(new { success = true, message = "Updated" });
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		await _service.DeleteAsync(id);
		return Ok(new { success = true, message = "Deleted (IsActive = false)" });
	}
}
