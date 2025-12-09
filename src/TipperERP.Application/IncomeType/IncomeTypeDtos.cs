using System;

namespace TipperERP.Application.IncomeType;

public class IncomeTypeListDto
{
	public Guid IncomeTypeId { get; set; }
	public string IncomeTypeName { get; set; } = string.Empty;
	public string? Description { get; set; }
	public bool IsActive { get; set; }
}

public class IncomeTypeCreateDto
{
	public string IncomeTypeName { get; set; } = string.Empty;
	public string? Description { get; set; }
	public bool IsActive { get; set; } = true;
}

public class IncomeTypeUpdateDto : IncomeTypeCreateDto
{
}
