using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.TipperDocument;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;

namespace TipperERP.Infrastructure.Services;

public class TipperDocumentService : ITipperDocumentService
{
	private readonly TipperErpDbContext _db;

	public TipperDocumentService(TipperErpDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<TipperDocumentListDto>> GetAllAsync()
	{
		return await _db.TipperDocuments
			.Include(d => d.Tipper)
			.Include(d => d.DocumentType)
			.Include(d => d.InsuranceCompany)
			.Where(d => !d.IsDeleted)
			.OrderBy(d => d.DocumentType.DocumentTypeName)
			.Select(d => new TipperDocumentListDto
			{
				TipperDocumentId = d.TipperDocumentId,
				TipperId = d.TipperId,
				TipperNumber = d.Tipper.TipperNumber,
				DocumentTypeId = d.DocumentTypeId,
				DocumentTypeName = d.DocumentType.DocumentTypeName,
				DocumentNumber = d.DocumentNumber,
				IssueDate = d.IssueDate,
				ExpiryDate = d.ExpiryDate,
				InsuranceCompanyId = d.InsuranceCompanyId,
				InsuranceCompanyName = d.InsuranceCompany != null ? d.InsuranceCompany.FinancierName : null,
				Remarks = d.Remarks,
				IsActive = d.IsActive
			})
			.ToListAsync();
	}

	public async Task<TipperDocumentListDto?> GetByIdAsync(Guid documentId)
	{
		return await _db.TipperDocuments
			.Include(d => d.Tipper)
			.Include(d => d.DocumentType)
			.Include(d => d.InsuranceCompany)
			.Where(d => d.TipperDocumentId == documentId && !d.IsDeleted)
			.Select(d => new TipperDocumentListDto
			{
				TipperDocumentId = d.TipperDocumentId,
				TipperId = d.TipperId,
				TipperNumber = d.Tipper.TipperNumber,
				DocumentTypeId = d.DocumentTypeId,
				DocumentTypeName = d.DocumentType.DocumentTypeName,
				DocumentNumber = d.DocumentNumber,
				IssueDate = d.IssueDate,
				ExpiryDate = d.ExpiryDate,
				InsuranceCompanyId = d.InsuranceCompanyId,
				InsuranceCompanyName = d.InsuranceCompany != null ? d.InsuranceCompany.FinancierName : null,
				Remarks = d.Remarks,
				IsActive = d.IsActive
			})
			.FirstOrDefaultAsync();
	}

	public async Task<Guid> CreateAsync(TipperDocumentCreateDto dto, Guid createdBy)
	{
		var entity = new TipperDocumentMaster
		{
			TipperDocumentId = Guid.NewGuid(),
			TipperId = dto.TipperId,
			DocumentTypeId = dto.DocumentTypeId,
			InsuranceCompanyId = dto.InsuranceCompanyId,
			DocumentNumber = dto.DocumentNumber,
			IssueDate = dto.IssueDate,
			ExpiryDate = dto.ExpiryDate,
			Remarks = dto.Remarks,
			IsActive = dto.IsActive,
			CreatedBy = createdBy
		};

		_db.TipperDocuments.Add(entity);
		await _db.SaveChangesAsync();

		return entity.TipperDocumentId;
	}

	public async Task UpdateAsync(Guid documentId, TipperDocumentUpdateDto dto, Guid updatedBy)
	{
		var entity = await _db.TipperDocuments.FirstOrDefaultAsync(d => d.TipperDocumentId == documentId && !d.IsDeleted);
		if (entity == null)
			throw new KeyNotFoundException("Document not found");

		entity.TipperId = dto.TipperId;
		entity.DocumentTypeId = dto.DocumentTypeId;
		entity.InsuranceCompanyId = dto.InsuranceCompanyId;
		entity.DocumentNumber = dto.DocumentNumber;
		entity.IssueDate = dto.IssueDate;
		entity.ExpiryDate = dto.ExpiryDate;
		entity.Remarks = dto.Remarks;
		entity.IsActive = dto.IsActive;

		entity.UpdatedBy = updatedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid documentId, Guid deletedBy)
	{
		var entity = await _db.TipperDocuments.FirstOrDefaultAsync(d => d.TipperDocumentId == documentId && !d.IsDeleted);
		if (entity == null) return;

		entity.IsDeleted = true;
		entity.UpdatedBy = deletedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}
}
