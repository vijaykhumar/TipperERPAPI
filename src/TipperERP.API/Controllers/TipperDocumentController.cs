using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipperERP.Application.TipperDocument;

namespace TipperERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TipperDocumentController : ControllerBase
{
	private readonly ITipperDocumentService _service;

	public TipperDocumentController(ITipperDocumentService service)
	{
		_service = service;
	}

	[HttpGet("list")]
	public async Task<IActionResult> GetAll()
	{
		var data = await _service.GetAllAsync();
		return Ok(new { success = true, data });
	}

	[HttpGet("{documentId:guid}")]
	public async Task<IActionResult> GetById(Guid documentId)
	{
		var data = await _service.GetByIdAsync(documentId);
		if (data == null)
			return NotFound(new { success = false, message = "Document not found" });

		return Ok(new { success = true, data });
	}

	[HttpPost]
	public async Task<IActionResult> Create(TipperDocumentCreateDto dto)
	{
		var createdBy = Guid.Empty; // replace later with JWT userId
		var id = await _service.CreateAsync(dto, createdBy);

		return Ok(new { success = true, id });
	}

	[HttpPut("{documentId:guid}")]
	public async Task<IActionResult> Update(Guid documentId, TipperDocumentUpdateDto dto)
	{
		var updatedBy = Guid.Empty;
		await _service.UpdateAsync(documentId, dto, updatedBy);

		return Ok(new { success = true, message = "Updated" });
	}

	[HttpDelete("{documentId:guid}")]
	public async Task<IActionResult> Delete(Guid documentId)
	{
		var deletedBy = Guid.Empty;
		await _service.DeleteAsync(documentId, deletedBy);

		return Ok(new { success = true, message = "Deleted" });
	}
}
