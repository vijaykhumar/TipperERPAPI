using System;

namespace TipperERP.Domain.Entities;

public class IncomeTypeMaster : BaseEntity
{
	public Guid IncomeTypeId { get; set; }
	public string IncomeTypeName { get; set; } = string.Empty;
	public string? Description { get; set; }

	public bool IsActive { get; set; } = true;
}
