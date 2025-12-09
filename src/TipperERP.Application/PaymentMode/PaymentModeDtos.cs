using System;

namespace TipperERP.Application.PaymentMode;

public class PaymentModeListDto
{
	public Guid PaymentModeId { get; set; }
	public string PaymentModeName { get; set; } = string.Empty;
	public string? Description { get; set; }
	public bool IsActive { get; set; }
}

public class PaymentModeCreateDto
{
	public string PaymentModeName { get; set; } = string.Empty;
	public string? Description { get; set; }
	public bool IsActive { get; set; } = true;
}

public class PaymentModeUpdateDto : PaymentModeCreateDto
{
}
