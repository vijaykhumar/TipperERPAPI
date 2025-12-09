using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipperERP.Application.Financier;

namespace TipperERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FinancierController : ControllerBase
{
	private readonly IFinancierService _service;

	public FinancierController(IFinancierService service)
	{
		_service = service;
	}

	[HttpGet("list")]
	public async Task<IActionResult> GetAll()
	{
		var data = await _service.GetAllAsync();
		return Ok(new { success = true, data });
	}

	[HttpGet("{financierId:guid}")]
	public async Task<IActionResult> GetById(Guid financierId)
	{
		var data = await _service.GetByIdAsync(financierId);
		if (data == null)
			return NotFound(new { success = false, message = "Financier not found" });

		return Ok(new { success = true, data });
	}

	[HttpPost]
	public async Task<IActionResult> Create(FinancierCreateDto dto)
	{
		var createdBy = Guid.Empty; // Replace later with JWT user
		var id = await _service.CreateAsync(dto, createdBy);

		return Ok(new { success = true, id });
	}

	[HttpPut("{financierId:guid}")]
	public async Task<IActionResult> Update(Guid financierId, FinancierUpdateDto dto)
	{
		var updatedBy = Guid.Empty;
		await _service.UpdateAsync(financierId, dto, updatedBy);

		return Ok(new { success = true, message = "Updated" });
	}

	[HttpDelete("{financierId:guid}")]
	public async Task<IActionResult> Delete(Guid financierId)
	{
		var deletedBy = Guid.Empty;
		await _service.DeleteAsync(financierId, deletedBy);

		return Ok(new { success = true, message = "Deleted" });
	}
}
