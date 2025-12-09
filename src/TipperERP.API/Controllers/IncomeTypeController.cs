using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipperERP.Application.IncomeType;

namespace TipperERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class IncomeTypeController : ControllerBase
{
	private readonly IIncomeTypeService _service;

	public IncomeTypeController(IIncomeTypeService service)
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
			return NotFound(new { success = false, message = "Income Type not found" });

		return Ok(new { success = true, data });
	}

	[HttpPost]
	public async Task<IActionResult> Create(IncomeTypeCreateDto dto)
	{
		var createdBy = Guid.Empty; // Replace with JWT later
		var id = await _service.CreateAsync(dto, createdBy);

		return Ok(new { success = true, id });
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> Update(Guid id, IncomeTypeUpdateDto dto)
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
