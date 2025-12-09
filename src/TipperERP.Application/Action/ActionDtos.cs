using System;

namespace TipperERP.Application.Action;

public class ActionListDto
{
	public Guid ActionId { get; set; }
	public string ActionName { get; set; } = string.Empty;
	public string? Description { get; set; }
	public bool IsActive { get; set; }
}

public class ActionCreateDto
{
	public string ActionName { get; set; } = string.Empty;
	public string? Description { get; set; }
	public bool IsActive { get; set; } = true;
}

public class ActionUpdateDto : ActionCreateDto
{
}
