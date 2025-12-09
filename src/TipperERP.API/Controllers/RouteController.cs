using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipperERP.Application.Route;

namespace TipperERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RouteController : ControllerBase
{
	private readonly IRouteService _service;

	public RouteController(IRouteService service)
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
			return NotFound(new { success = false, message = "Route not found" });

		return Ok(new { success = true, data });
	}

	[HttpPost]
	public async Task<IActionResult> Create(RouteCreateDto dto)
	{
		var createdBy = Guid.Empty; // TODO: map from logged-in JWT later
		var newId = await _service.CreateAsync(dto, createdBy);

		return Ok(new { success = true, id = newId });
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> Update(Guid id, RouteUpdateDto dto)
	{
		var updatedBy = Guid.Empty;
		await _service.UpdateAsync(id, dto, updatedBy);

		return Ok(new { success = true, message = "Updated" });
	}

	[HttpDelete("{id:guid}")]
	public async Task<IActionResult> Delete(Guid id)
	{
		var deletedBy = Guid.Empty;
		await _service.DeleteAsync(id, deletedBy);

		return Ok(new { success = true, message = "Deleted" });
	}
}
