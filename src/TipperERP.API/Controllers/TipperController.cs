using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipperERP.Application.Tipper;

namespace TipperERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TipperController : ControllerBase
{
	private readonly ITipperService _service;

	public TipperController(ITipperService service)
	{
		_service = service;
	}

	[HttpGet("list")]
	public async Task<IActionResult> GetAll()
	{
		var data = await _service.GetAllAsync();
		return Ok(new { success = true, data });
	}

	[HttpGet("{tipperId:guid}")]
	public async Task<IActionResult> GetById(Guid tipperId)
	{
		var data = await _service.GetByIdAsync(tipperId);
		if (data == null)
			return NotFound(new { success = false, message = "Tipper not found" });

		return Ok(new { success = true, data });
	}

	[HttpPost]
	public async Task<IActionResult> Create(TipperCreateDto dto)
	{
		var createdBy = Guid.Empty; // Replace with JWT user later
		var id = await _service.CreateAsync(dto, createdBy);

		return Ok(new { success = true, id });
	}

	[HttpPut("{tipperId:guid}")]
	public async Task<IActionResult> Update(Guid tipperId, TipperUpdateDto dto)
	{
		var updatedBy = Guid.Empty;
		await _service.UpdateAsync(tipperId, dto, updatedBy);

		return Ok(new { success = true, message = "Updated" });
	}

	[HttpDelete("{tipperId:guid}")]
	public async Task<IActionResult> Delete(Guid tipperId)
	{
		var deletedBy = Guid.Empty;
		await _service.DeleteAsync(tipperId, deletedBy);

		return Ok(new { success = true, message = "Deleted" });
	}
}
