using System;

namespace TipperERP.Domain.Entities;

public class MaterialTypeMaster : BaseEntity
{
	public Guid MaterialTypeId { get; set; }
	public string MaterialCode { get; set; } = string.Empty;
	public string MaterialName { get; set; } = string.Empty;
	public string? Description { get; set; }
	public bool IsActive { get; set; } = true;
}
