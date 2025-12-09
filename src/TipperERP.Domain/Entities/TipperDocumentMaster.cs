using System;

namespace TipperERP.Domain.Entities;

public class TipperDocumentMaster : BaseEntity
{
	public Guid TipperDocumentId { get; set; }
	public Guid TipperId { get; set; }
	public Guid DocumentTypeId { get; set; }
	public Guid? InsuranceCompanyId { get; set; }

	public string? DocumentNumber { get; set; }
	public DateTime? IssueDate { get; set; }
	public DateTime? ExpiryDate { get; set; }
	public string? Remarks { get; set; }

	public bool IsActive { get; set; } = true;

	public TipperMaster Tipper { get; set; } = null!;
	public DocumentTypeMaster DocumentType { get; set; } = null!;
	public FinancierMaster? InsuranceCompany { get; set; }
}
