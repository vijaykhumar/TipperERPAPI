using System;

namespace TipperERP.Application.Site;

public class SiteListDto
{
	public Guid SiteId { get; set; }
	public Guid CustomerId { get; set; }
	public string CustomerName { get; set; } = string.Empty;
	public string SiteName { get; set; } = string.Empty;
	public string? Address { get; set; }
	public bool IsActive { get; set; }
}

public class SiteCreateDto
{
	public Guid CustomerId { get; set; }
	public string SiteName { get; set; } = string.Empty;
	public string? Address { get; set; }
	public bool IsActive { get; set; } = true;
}

public class SiteUpdateDto : SiteCreateDto
{
}
