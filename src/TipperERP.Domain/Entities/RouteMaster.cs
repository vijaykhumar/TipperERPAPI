using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TipperERP.Domain.Entities;

public class RouteMaster : BaseEntity
{
	public Guid RouteId { get; set; }
	public Guid? CustomerId { get; set; }
	public Guid? SiteId { get; set; }
	public Guid? DumpingId { get; set; }

	public string RouteName { get; set; } = string.Empty;
	public decimal ApproxDistanceKm { get; set; }
	public bool IsActive { get; set; } = true;

	public CustomerMaster? Customer { get; set; }
	public SiteMaster? Site { get; set; }
	public DumpingMaster? Dumping { get; set; }
}

