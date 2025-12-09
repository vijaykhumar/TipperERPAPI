using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.DriverDocument;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;

namespace TipperERP.Infrastructure.Services;

public class DriverDocumentService : IDriverDocumentService
{
	private readonly TipperErpDbContext _db;

	public DriverDocumentService(TipperErpDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<DriverDocumentListDto>> GetAllAsync()
	{
		return await _db.DriverDocuments
			.Include(d => d.Driver)
			.Include(d => d.DocumentType)
			.Where(d => !d.IsDeleted)
			.OrderBy(d => d.Driver.DriverName)
			.Select(d => new DriverDocumentListDto
			{
				DriverDocumentId = d.DriverDocumentId,
				DriverId = d.DriverId,
				DriverName = d.Driver.DriverName,
				DocumentTypeId = d.DocumentTypeId,
				DocumentTypeName = d.DocumentType.DocumentTypeName,
				DocumentNumber = d.DocumentNumber,
				IssueDate = d.IssueDate,
				ExpiryDate = d.ExpiryDate,
				Remarks = d.Remarks,
				IsActive = d.IsActive
			})
			.ToListAsync();
	}

	public async Task<DriverDocumentListDto?> GetByIdAsync(Guid id)
	{
		return await _db.DriverDocuments
			.Include(d => d.Driver)
			.Include(d => d.DocumentType)
			.Where(d => d.DriverDocumentId == id && !d.IsDeleted)
			.Select(d => new DriverDocumentListDto
			{
				DriverDocumentId = d.DriverDocumentId,
				DriverId = d.DriverId,
				DriverName = d.Driver.DriverName,
				DocumentTypeId = d.DocumentTypeId,
				DocumentTypeName = d.DocumentType.DocumentTypeName,
				DocumentNumber = d.DocumentNumber,
				IssueDate = d.IssueDate,
				ExpiryDate = d.ExpiryDate,
				Remarks = d.Remarks,
				IsActive = d.IsActive
			})
			.FirstOrDefaultAsync();
	}

	public async Task<Guid> CreateAsync(DriverDocumentCreateDto dto, Guid createdBy)
	{
		var entity = new DriverDocumentMaster
		{
			DriverDocumentId = Guid.NewGuid(),
			DriverId = dto.DriverId,
			DocumentTypeId = dto.DocumentTypeId,
			DocumentNumber = dto.DocumentNumber,
			IssueDate = dto.IssueDate,
			ExpiryDate = dto.ExpiryDate,
			Remarks = dto.Remarks,
			IsActive = dto.IsActive,
			CreatedBy = createdBy
		};

		_db.DriverDocuments.Add(entity);
		await _db.SaveChangesAsync();

		return entity.DriverDocumentId;
	}

	public async Task UpdateAsync(Guid id, DriverDocumentUpdateDto dto, Guid updatedBy)
	{
		var entity = await _db.DriverDocuments.FirstOrDefaultAsync(d => d.DriverDocumentId == id && !d.IsDeleted);
		if (entity == null)
			throw new KeyNotFoundException("Driver document not found");

		entity.DriverId = dto.DriverId;
		entity.DocumentTypeId = dto.DocumentTypeId;
		entity.DocumentNumber = dto.DocumentNumber;
		entity.IssueDate = dto.IssueDate;
		entity.ExpiryDate = dto.ExpiryDate;
		entity.Remarks = dto.Remarks;
		entity.IsActive = dto.IsActive;

		entity.UpdatedBy = updatedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id, Guid deletedBy)
	{
		var entity = await _db.DriverDocuments.FirstOrDefaultAsync(d => d.DriverDocumentId == id && !d.IsDeleted);
		if (entity == null) return;

		entity.IsDeleted = true;
		entity.UpdatedBy = deletedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}
}
