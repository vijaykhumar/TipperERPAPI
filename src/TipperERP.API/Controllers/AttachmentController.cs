using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipperERP.Application.Attachment;

namespace TipperERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AttachmentController : ControllerBase
{
	private readonly IAttachmentService _service;

	public AttachmentController(IAttachmentService service)
	{
		_service = service;
	}

	[HttpGet("{entityType}/{entityId:guid}")]
	public async Task<IActionResult> GetByEntity(string entityType, Guid entityId)
	{
		var data = await _service.GetByEntityAsync(entityType, entityId);
		return Ok(new { success = true, data });
	}

	[HttpPost("upload/{entityType}/{entityId:guid}")]
	public async Task<IActionResult> Upload(string entityType, Guid entityId, IFormFile file)
	{
		var uploadedBy = Guid.Empty; // Replace with JWT UserId
		var result = await _service.UploadAsync(entityType, entityId, file, uploadedBy);

		return Ok(new { success = true, data = result });
	}

	[HttpDelete("{attachmentId:guid}")]
	public async Task<IActionResult> Delete(Guid attachmentId)
	{
		await _service.DeleteAsync(attachmentId);
		return Ok(new { success = true, message = "Attachment deleted" });
	}
}
