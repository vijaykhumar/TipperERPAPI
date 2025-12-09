using System;

namespace TipperERP.Domain.Entities;

public class InsuranceCompanyMaster : BaseEntity
{
	public Guid InsuranceCompanyId { get; set; }
	public string CompanyName { get; set; } = string.Empty;

	public string? ContactNumber { get; set; }
	public string? Email { get; set; }

	public bool IsActive { get; set; } = true;
}
