using System;

namespace TipperERP.Application.AuditLog;

public class AuditLogListDto
{
	public Guid AuditId { get; set; }
	public string EntityName { get; set; } = string.Empty;
	public Guid EntityId { get; set; }
	public string Action { get; set; } = string.Empty;
	public string? OldValueJson { get; set; }
	public string? NewValueJson { get; set; }
	public Guid PerformedBy { get; set; }
	public DateTime PerformedDate { get; set; }
}

public class AuditLogCreateDto
{
	public string EntityName { get; set; } = string.Empty;
	public Guid EntityId { get; set; }
	public string Action { get; set; } = string.Empty;
	public string? OldValueJson { get; set; }
	public string? NewValueJson { get; set; }
}
