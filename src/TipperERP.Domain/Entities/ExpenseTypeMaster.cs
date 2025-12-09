using System;

namespace TipperERP.Domain.Entities;

public class ExpenseTypeMaster : BaseEntity
{
	public Guid ExpenseTypeId { get; set; }
	public string ExpenseTypeName { get; set; } = string.Empty;
	public string? Category { get; set; }
	public string? Description { get; set; }

	public bool IsActive { get; set; } = true;
}
