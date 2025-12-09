using System;

namespace TipperERP.Domain.Entities;

public class PaymentModeMaster : BaseEntity
{
	public Guid PaymentModeId { get; set; }
	public string PaymentModeName { get; set; } = string.Empty;
	public string? Description { get; set; }

	public bool IsActive { get; set; } = true;
}
