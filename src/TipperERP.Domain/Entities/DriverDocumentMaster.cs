using System;

namespace TipperERP.Domain.Entities;

public class DriverDocumentMaster : BaseEntity
{
	public Guid DriverDocumentId { get; set; }
	public Guid DriverId { get; set; }
	public Guid DocumentTypeId { get; set; }

	public string? DocumentNumber { get; set; }
	public DateTime? IssueDate { get; set; }
	public DateTime? ExpiryDate { get; set; }
	public string? Remarks { get; set; }

	public bool IsActive { get; set; } = true;

	public DriverMaster Driver { get; set; } = null!;
	public DocumentTypeMaster DocumentType { get; set; } = null!;
}
