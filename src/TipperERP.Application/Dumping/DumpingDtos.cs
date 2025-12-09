using System;

namespace TipperERP.Application.Dumping;

public class DumpingListDto
{
	public Guid DumpingId { get; set; }
	public Guid? CustomerId { get; set; }
	public string? CustomerName { get; set; }
	public string DumpingName { get; set; } = string.Empty;
	public string? Address { get; set; }
	public bool IsActive { get; set; }
}

public class DumpingCreateDto
{
	public Guid? CustomerId { get; set; }
	public string DumpingName { get; set; } = string.Empty;
	public string? Address { get; set; }
	public bool IsActive { get; set; } = true;
}

public class DumpingUpdateDto : DumpingCreateDto
{
}

