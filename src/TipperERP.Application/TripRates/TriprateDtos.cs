using System;

namespace TipperERP.Application.TripRates;

public class TripRateListDto
{
	public Guid RateId { get; set; }

	public Guid? CustomerId { get; set; }
	public string? CustomerName { get; set; }

	public Guid? SiteId { get; set; }
	public string? SiteName { get; set; }

	public Guid? DumpingId { get; set; }
	public string? DumpingName { get; set; }

	public Guid? RouteId { get; set; }
	public string? RouteName { get; set; }

	public Guid? MaterialTypeId { get; set; }
	public string? MaterialTypeName { get; set; }

	public string RateType { get; set; } = string.Empty;
	public decimal RateValue { get; set; }
	public string? Currency { get; set; }
	public DateTime? EffectiveFrom { get; set; }
	public DateTime? EffectiveTo { get; set; }

	public bool IsActive { get; set; }
}

public class TripRateCreateDto
{
	public Guid? CustomerId { get; set; }
	public Guid? SiteId { get; set; }
	public Guid? DumpingId { get; set; }
	public Guid? RouteId { get; set; }
	public Guid? MaterialTypeId { get; set; }

	public string RateType { get; set; } = string.Empty;
	public decimal RateValue { get; set; }
	public string? Currency { get; set; }
	public DateTime? EffectiveFrom { get; set; }
	public DateTime? EffectiveTo { get; set; }

	public bool IsActive { get; set; } = true;
}

public class TripRateUpdateDto : TripRateCreateDto
{
}
