using System;

namespace TipperERP.Domain.Entities;

public class FinancierMaster : BaseEntity
{
	public Guid FinancierId { get; set; }
	public string FinancierName { get; set; } = string.Empty;
	public string? ContactPerson { get; set; }
	public string? Phone { get; set; }
	public string? Email { get; set; }
	public string? Address { get; set; }

	public bool IsActive { get; set; } = true;
}
