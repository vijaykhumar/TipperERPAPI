using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipperERP.Application.TripRates;

namespace TipperERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TripRateController : ControllerBase
{
	private readonly ITripRateService _service;

	public TripRateController(ITripRateService service)
	{
		_service = service;
	}

	[HttpGet("list")]
	public async Task<IActionResult> GetAll()
	{
		var data = await _service.GetAllAsync();
		return Ok(new { success = true, data });
	}

	[HttpGet("{rateId:guid}")]
	public async Task<IActionResult> GetById(Guid rateId)
	{
		var data = await _service.GetByIdAsync(rateId);
		if (data == null)
			return NotFound(new { success = false, message = "Trip rate not found" });

		return Ok(new { success = true, data });
	}

	[HttpPost]
	public async Task<IActionResult> Create(TripRateCreateDto dto)
	{
		var user = Guid.Empty; // TODO: Replace with JWT userId
		var id = await _service.CreateAsync(dto, user);

		return Ok(new { success = true, id });
	}

	[HttpPut("{rateId:guid}")]
	public async Task<IActionResult> Update(Guid rateId, TripRateUpdateDto dto)
	{
		var user = Guid.Empty;
		await _service.UpdateAsync(rateId, dto, user);

		return Ok(new { success = true, message = "Updated" });
	}

	[HttpDelete("{rateId:guid}")]
	public async Task<IActionResult> Delete(Guid rateId)
	{
		var user = Guid.Empty;
		await _service.DeleteAsync(rateId, user);

		return Ok(new { success = true, message = "Deleted" });
	}
}
