using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.DocumentType;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;

namespace TipperERP.Infrastructure.Services;

public class DocumentTypeService : IDocumentTypeService
{
	private readonly TipperErpDbContext _db;

	public DocumentTypeService(TipperErpDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<DocumentTypeListDto>> GetAllAsync()
	{
		return await _db.DocumentTypes
			.Where(d => !d.IsDeleted)
			.OrderBy(d => d.DocumentTypeName)
			.Select(d => new DocumentTypeListDto
			{
				DocumentTypeId = d.DocumentTypeId,
				DocumentTypeName = d.DocumentTypeName,
				Category = d.Category,
				IsActive = d.IsActive
			})
			.ToListAsync();
	}

	public async Task<DocumentTypeListDto?> GetByIdAsync(Guid id)
	{
		return await _db.DocumentTypes
			.Where(d => d.DocumentTypeId == id && !d.IsDeleted)
			.Select(d => new DocumentTypeListDto
			{
				DocumentTypeId = d.DocumentTypeId,
				DocumentTypeName = d.DocumentTypeName,
				Category = d.Category,
				IsActive = d.IsActive
			})
			.FirstOrDefaultAsync();
	}

	public async Task<Guid> CreateAsync(DocumentTypeCreateDto dto, Guid createdBy)
	{
		var entity = new DocumentTypeMaster
		{
			DocumentTypeId = Guid.NewGuid(),
			DocumentTypeName = dto.DocumentTypeName,
			Category = dto.Category,
			IsActive = dto.IsActive,
			CreatedBy = createdBy
		};

		_db.DocumentTypes.Add(entity);
		await _db.SaveChangesAsync();

		return entity.DocumentTypeId;
	}

	public async Task UpdateAsync(Guid id, DocumentTypeUpdateDto dto, Guid updatedBy)
	{
		var entity = await _db.DocumentTypes.FirstOrDefaultAsync(d => d.DocumentTypeId == id && !d.IsDeleted);
		if (entity == null)
			throw new KeyNotFoundException("Document type not found");

		entity.DocumentTypeName = dto.DocumentTypeName;
		entity.Category = dto.Category;
		entity.IsActive = dto.IsActive;
		entity.UpdatedBy = updatedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id, Guid deletedBy)
	{
		var entity = await _db.DocumentTypes.FirstOrDefaultAsync(d => d.DocumentTypeId == id && !d.IsDeleted);
		if (entity == null) return;

		entity.IsDeleted = true;
		entity.UpdatedDate = DateTime.UtcNow;
		entity.UpdatedBy = deletedBy;

		await _db.SaveChangesAsync();
	}
}
