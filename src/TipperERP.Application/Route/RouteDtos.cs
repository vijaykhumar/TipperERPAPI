using System;

namespace TipperERP.Application.Route;

public class RouteListDto
{
	public Guid RouteId { get; set; }
	public Guid? CustomerId { get; set; }
	public string? CustomerName { get; set; }

	public Guid? SiteId { get; set; }
	public string? SiteName { get; set; }

	public Guid? DumpingId { get; set; }
	public string? DumpingName { get; set; }

	public string RouteName { get; set; } = string.Empty;
	public decimal ApproxDistanceKm { get; set; }
	public bool IsActive { get; set; }
}

public class RouteCreateDto
{
	public Guid? CustomerId { get; set; }
	public Guid? SiteId { get; set; }
	public Guid? DumpingId { get; set; }

	public string RouteName { get; set; } = string.Empty;
	public decimal ApproxDistanceKm { get; set; }
	public bool IsActive { get; set; } = true;
}

public class RouteUpdateDto : RouteCreateDto
{
}
