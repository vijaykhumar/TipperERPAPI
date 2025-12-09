using System;

namespace TipperERP.Application.Tax;

public class TaxListDto
{
	public Guid TaxId { get; set; }
	public string TaxName { get; set; } = string.Empty;
	public decimal TaxPercentage { get; set; }
	public DateTime EffectiveFrom { get; set; }
	public DateTime? EffectiveTo { get; set; }
	public string? Country { get; set; }
	public bool IsActive { get; set; }
}

public class TaxCreateDto
{
	public string TaxName { get; set; } = string.Empty;
	public decimal TaxPercentage { get; set; }
	public DateTime EffectiveFrom { get; set; }
	public DateTime? EffectiveTo { get; set; }
	public string? Country { get; set; }
	public bool IsActive { get; set; } = true;
}

public class TaxUpdateDto : TaxCreateDto
{
}
