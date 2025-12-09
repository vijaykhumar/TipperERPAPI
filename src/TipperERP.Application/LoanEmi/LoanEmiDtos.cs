using System;

namespace TipperERP.Application.LoanEmi;

public class LoanEmiListDto
{
	public Guid EmiId { get; set; }

	public Guid TipperId { get; set; }
	public string TipperNumber { get; set; } = string.Empty;

	public Guid FinancierId { get; set; }
	public string FinancierName { get; set; } = string.Empty;

	public DateTime EmiStartDate { get; set; }
	public DateTime EmiEndDate { get; set; }
	public decimal MonthlyEmiAmount { get; set; }
	public int? TenureMonths { get; set; }

	public string? Remarks { get; set; }
	public bool IsActive { get; set; }
}

public class LoanEmiCreateDto
{
	public Guid TipperId { get; set; }
	public Guid FinancierId { get; set; }

	public DateTime EmiStartDate { get; set; }
	public DateTime EmiEndDate { get; set; }
	public decimal MonthlyEmiAmount { get; set; }
	public int? TenureMonths { get; set; }

	public string? Remarks { get; set; }
	public bool IsActive { get; set; } = true;
}

public class LoanEmiUpdateDto : LoanEmiCreateDto
{
}
