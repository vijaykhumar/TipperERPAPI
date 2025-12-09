using System;

namespace TipperERP.Application.Financier;

public class FinancierListDto
{
	public Guid FinancierId { get; set; }
	public string FinancierName { get; set; } = string.Empty;
	public string? ContactPerson { get; set; }
	public string? Phone { get; set; }
	public string? Email { get; set; }
	public string? Address { get; set; }
	public bool IsActive { get; set; }
}

public class FinancierCreateDto
{
	public string FinancierName { get; set; } = string.Empty;
	public string? ContactPerson { get; set; }
	public string? Phone { get; set; }
	public string? Email { get; set; }
	public string? Address { get; set; }
	public bool IsActive { get; set; } = true;
}

public class FinancierUpdateDto : FinancierCreateDto
{
}
