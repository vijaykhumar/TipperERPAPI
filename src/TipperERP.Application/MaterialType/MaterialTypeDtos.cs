using System;

namespace TipperERP.Application.MaterialType;

public class MaterialTypeListDto
{
	public Guid MaterialTypeId { get; set; }
	public string MaterialCode { get; set; } = string.Empty;
	public string MaterialName { get; set; } = string.Empty;
	public string? Description { get; set; }
	public bool IsActive { get; set; }
}

public class MaterialTypeCreateDto
{
	public string MaterialCode { get; set; } = string.Empty;
	public string MaterialName { get; set; } = string.Empty;
	public string? Description { get; set; }
	public bool IsActive { get; set; } = true;
}

public class MaterialTypeUpdateDto : MaterialTypeCreateDto
{
}

