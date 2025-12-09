using System;

namespace TipperERP.Domain.Entities;

public class AuditLogMaster
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
