using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TipperERP.Domain.Entities;

public class SiteMaster : BaseEntity
{
	public Guid SiteId { get; set; }
	public Guid CustomerId { get; set; }
	public string SiteName { get; set; } = string.Empty;
	public string? Address { get; set; }
	public bool IsActive { get; set; } = true;

	public CustomerMaster Customer { get; set; } = null!;
}


