using System;

namespace TipperERP.Domain.Entities;

public class TaxMaster : BaseEntity
{
	public Guid TaxId { get; set; }
	public string TaxName { get; set; } = string.Empty;
	public decimal TaxPercentage { get; set; }
	public DateTime EffectiveFrom { get; set; }
	public DateTime? EffectiveTo { get; set; }
	public string? Country { get; set; }
	public bool IsActive { get; set; } = true;
}
