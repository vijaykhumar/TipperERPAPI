using System;

namespace TipperERP.Application.ExpenseType;

public class ExpenseTypeListDto
{
	public Guid ExpenseTypeId { get; set; }
	public string ExpenseTypeName { get; set; } = string.Empty;
	public string? Category { get; set; }
	public string? Description { get; set; }
	public bool IsActive { get; set; }
}

public class ExpenseTypeCreateDto
{
	public string ExpenseTypeName { get; set; } = string.Empty;
	public string? Category { get; set; }
	public string? Description { get; set; }
	public bool IsActive { get; set; } = true;
}

public class ExpenseTypeUpdateDto : ExpenseTypeCreateDto
{
}
