using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipperERP.Application.Dumping;

namespace TipperERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DumpingController : ControllerBase
{
	private readonly IDumpingService _service;

	public DumpingController(IDumpingService service)
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
			return NotFound(new { success = false, message = "Dumping site not found" });

		return Ok(new { success = true, data });
	}

	[HttpPost]
	public async Task<IActionResult> Create(DumpingCreateDto dto)
	{
		var createdBy = Guid.Empty; // TODO extract from JWT
		var newId = await _service.CreateAsync(dto, createdBy);

		return Ok(new { success = true, id = newId });
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> Update(Guid id, DumpingUpdateDto dto)
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
