using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.MaterialType;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;

namespace TipperERP.Infrastructure.Services;

public class MaterialTypeService : IMaterialTypeService
{
	private readonly TipperErpDbContext _db;

	public MaterialTypeService(TipperErpDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<MaterialTypeListDto>> GetAllAsync()
	{
		return await _db.MaterialTypes
			.Where(m => !m.IsDeleted)
			.OrderBy(m => m.MaterialName)
			.Select(m => new MaterialTypeListDto
			{
				MaterialTypeId = m.MaterialTypeId,
				MaterialCode = m.MaterialCode,
				MaterialName = m.MaterialName,
				Description = m.Description,
				IsActive = m.IsActive
			})
			.ToListAsync();
	}

	public async Task<MaterialTypeListDto?> GetByIdAsync(Guid id)
	{
		return await _db.MaterialTypes
			.Where(m => m.MaterialTypeId == id && !m.IsDeleted)
			.Select(m => new MaterialTypeListDto
			{
				MaterialTypeId = m.MaterialTypeId,
				MaterialCode = m.MaterialCode,
				MaterialName = m.MaterialName,
				Description = m.Description,
				IsActive = m.IsActive
			})
			.FirstOrDefaultAsync();
	}

	public async Task<Guid> CreateAsync(MaterialTypeCreateDto dto, Guid createdBy)
	{
		var entity = new MaterialTypeMaster
		{
			MaterialTypeId = Guid.NewGuid(),
			MaterialCode = dto.MaterialCode,
			MaterialName = dto.MaterialName,
			Description = dto.Description,
			IsActive = dto.IsActive,
			CreatedBy = createdBy
		};

		_db.MaterialTypes.Add(entity);
		await _db.SaveChangesAsync();

		return entity.MaterialTypeId;
	}

	public async Task UpdateAsync(Guid id, MaterialTypeUpdateDto dto, Guid updatedBy)
	{
		var entity = await _db.MaterialTypes.FirstOrDefaultAsync(m => m.MaterialTypeId == id && !m.IsDeleted);
		if (entity == null) throw new KeyNotFoundException("Material type not found");

		entity.MaterialCode = dto.MaterialCode;
		entity.MaterialName = dto.MaterialName;
		entity.Description = dto.Description;
		entity.IsActive = dto.IsActive;
		entity.UpdatedBy = updatedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id, Guid deletedBy)
	{
		var entity = await _db.MaterialTypes.FirstOrDefaultAsync(m => m.MaterialTypeId == id && !m.IsDeleted);
		if (entity == null) return;

		entity.IsDeleted = true;
		entity.UpdatedBy = deletedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}
}
