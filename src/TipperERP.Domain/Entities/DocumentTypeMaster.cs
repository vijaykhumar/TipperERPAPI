using System;

namespace TipperERP.Domain.Entities;

public class DocumentTypeMaster : BaseEntity
{
	public Guid DocumentTypeId { get; set; }
	public string DocumentTypeName { get; set; } = string.Empty;
	public string? Category { get; set; }

	public bool IsActive { get; set; } = true;
}
