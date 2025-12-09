using System;

namespace TipperERP.Application.DocumentType;

public class DocumentTypeListDto
{
	public Guid DocumentTypeId { get; set; }
	public string DocumentTypeName { get; set; } = string.Empty;
	public string? Category { get; set; }
	public bool IsActive { get; set; }
}

public class DocumentTypeCreateDto
{
	public string DocumentTypeName { get; set; } = string.Empty;
	public string? Category { get; set; }
	public bool IsActive { get; set; } = true;
}

public class DocumentTypeUpdateDto : DocumentTypeCreateDto
{
}
