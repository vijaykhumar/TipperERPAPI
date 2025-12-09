using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipperERP.Application.Tax;

namespace TipperERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaxController : ControllerBase
{
	private readonly ITaxService _service;

	public TaxController(ITaxService service)
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
			return NotFound(new { success = false, message = "Tax not found" });

		return Ok(new { success = true, data });
	}

	[HttpPost]
	public async Task<IActionResult> Create(TaxCreateDto dto)
	{
		var createdBy = Guid.Empty;
		var id = await _service.CreateAsync(dto, createdBy);

		return Ok(new { success = true, id });
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> Update(Guid id, TaxUpdateDto dto)
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
