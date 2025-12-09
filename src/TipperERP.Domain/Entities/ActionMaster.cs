using System;

namespace TipperERP.Domain.Entities;

public class ActionMaster
{
	public Guid ActionId { get; set; }
	public string ActionName { get; set; } = string.Empty;
	public string? Description { get; set; }
	public bool IsActive { get; set; } = true;
}
