using System;

namespace TipperERP.Domain.Entities;

public class LoanEmiMaster : BaseEntity
{
	public Guid EmiId { get; set; }
	public Guid TipperId { get; set; }
	public Guid FinancierId { get; set; }

	public DateTime EmiStartDate { get; set; }
	public DateTime EmiEndDate { get; set; }
	public decimal MonthlyEmiAmount { get; set; }
	public int? TenureMonths { get; set; }

	public string? Remarks { get; set; }

	public bool IsActive { get; set; } = true;

	public TipperMaster Tipper { get; set; } = null!;
	public FinancierMaster Financier { get; set; } = null!;
}
