using System;

namespace TipperERP.Application.TipperDocument;

public class TipperDocumentListDto
{
	public Guid TipperDocumentId { get; set; }
	public Guid TipperId { get; set; }
	public string TipperNumber { get; set; } = string.Empty;

	public Guid DocumentTypeId { get; set; }
	public string DocumentTypeName { get; set; } = string.Empty;

	public string? DocumentNumber { get; set; }
	public DateTime? IssueDate { get; set; }
	public DateTime? ExpiryDate { get; set; }

	public Guid? InsuranceCompanyId { get; set; }
	public string? InsuranceCompanyName { get; set; }

	public string? Remarks { get; set; }
	public bool IsActive { get; set; }
}

public class TipperDocumentCreateDto
{
	public Guid TipperId { get; set; }
	public Guid DocumentTypeId { get; set; }
	public Guid? InsuranceCompanyId { get; set; }

	public string? DocumentNumber { get; set; }
	public DateTime? IssueDate { get; set; }
	public DateTime? ExpiryDate { get; set; }
	public string? Remarks { get; set; }

	public bool IsActive { get; set; } = true;
}

public class TipperDocumentUpdateDto : TipperDocumentCreateDto
{
}
