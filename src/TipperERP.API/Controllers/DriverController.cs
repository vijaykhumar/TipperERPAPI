using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipperERP.Application.Driver;

namespace TipperERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DriverController : ControllerBase
{
	private readonly IDriverService _service;

	public DriverController(IDriverService service)
	{
		_service = service;
	}

	[HttpGet("list")]
	public async Task<IActionResult> GetAll()
	{
		var result = await _service.GetAllAsync();
		return Ok(new { success = true, data = result });
	}

	[HttpGet("{driverId:guid}")]
	public async Task<IActionResult> GetById(Guid driverId)
	{
		var result = await _service.GetByIdAsync(driverId);
		if (result == null)
			return NotFound(new { success = false, message = "Driver not found" });

		return Ok(new { success = true, data = result });
	}

	[HttpPost]
	public async Task<IActionResult> Create(DriverCreateDto dto)
	{
		var createdBy = Guid.Empty; // Replace with JWT User ID later
		var id = await _service.CreateAsync(dto, createdBy);
		return Ok(new { success = true, id });
	}

	[HttpPut("{driverId:guid}")]
	public async Task<IActionResult> Update(Guid driverId, DriverUpdateDto dto)
	{
		var updatedBy = Guid.Empty;
		await _service.UpdateAsync(driverId, dto, updatedBy);

		return Ok(new { success = true, message = "Updated" });
	}

	[HttpDelete("{driverId:guid}")]
	public async Task<IActionResult> Delete(Guid driverId)
	{
		var deletedBy = Guid.Empty;
		await _service.DeleteAsync(driverId, deletedBy);

		return Ok(new { success = true, message = "Deleted" });
	}
}
