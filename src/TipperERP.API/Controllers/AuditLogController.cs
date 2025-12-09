using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TipperERP.Application.AuditLog;

namespace TipperERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuditLogController : ControllerBase
{
	private readonly IAuditLogService _service;

	public AuditLogController(IAuditLogService service)
	{
		_service = service;
	}

	[HttpGet("list")]
	public async Task<IActionResult> GetAll()
	{
		var logs = await _service.GetAllAsync();
		return Ok(new { success = true, data = logs });
	}

	[HttpGet("{entityName}/{entityId:guid}")]
	public async Task<IActionResult> GetByEntity(string entityName, Guid entityId)
	{
		var logs = await _service.GetByEntityAsync(entityName, entityId);
		return Ok(new { success = true, data = logs });
	}
}
