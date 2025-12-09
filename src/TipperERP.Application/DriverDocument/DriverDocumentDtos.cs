using System;

namespace TipperERP.Application.DriverDocument;

public class DriverDocumentListDto
{
	public Guid DriverDocumentId { get; set; }

	public Guid DriverId { get; set; }
	public string DriverName { get; set; } = string.Empty;

	public Guid DocumentTypeId { get; set; }
	public string DocumentTypeName { get; set; } = string.Empty;

	public string? DocumentNumber { get; set; }
	public DateTime? IssueDate { get; set; }
	public DateTime? ExpiryDate { get; set; }

	public string? Remarks { get; set; }
	public bool IsActive { get; set; }
}

public class DriverDocumentCreateDto
{
	public Guid DriverId { get; set; }
	public Guid DocumentTypeId { get; set; }

	public string? DocumentNumber { get; set; }
	public DateTime? IssueDate { get; set; }
	public DateTime? ExpiryDate { get; set; }
	public string? Remarks { get; set; }

	public bool IsActive { get; set; } = true;
}

public class DriverDocumentUpdateDto : DriverDocumentCreateDto
{
}
