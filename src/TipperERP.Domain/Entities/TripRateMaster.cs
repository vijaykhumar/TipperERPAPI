using System;

namespace TipperERP.Domain.Entities;

public class TripRateMaster : BaseEntity
{
	public Guid RateId { get; set; }

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

	public CustomerMaster? Customer { get; set; }
	public SiteMaster? Site { get; set; }
	public DumpingMaster? Dumping { get; set; }
	public RouteMaster? Route { get; set; }
	public MaterialTypeMaster? MaterialType { get; set; }
}
