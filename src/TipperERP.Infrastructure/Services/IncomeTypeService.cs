using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.IncomeType;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;

namespace TipperERP.Infrastructure.Services;

public class IncomeTypeService : IIncomeTypeService
{
	private readonly TipperErpDbContext _db;

	public IncomeTypeService(TipperErpDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<IncomeTypeListDto>> GetAllAsync()
	{
		return await _db.IncomeTypes
			.Where(x => !x.IsDeleted)
			.OrderBy(x => x.IncomeTypeName)
			.Select(x => new IncomeTypeListDto
			{
				IncomeTypeId = x.IncomeTypeId,
				IncomeTypeName = x.IncomeTypeName,
				Description = x.Description,
				IsActive = x.IsActive
			})
			.ToListAsync();
	}

	public async Task<IncomeTypeListDto?> GetByIdAsync(Guid id)
	{
		return await _db.IncomeTypes
			.Where(x => x.IncomeTypeId == id && !x.IsDeleted)
			.Select(x => new IncomeTypeListDto
			{
				IncomeTypeId = x.IncomeTypeId,
				IncomeTypeName = x.IncomeTypeName,
				Description = x.Description,
				IsActive = x.IsActive
			})
			.FirstOrDefaultAsync();
	}

	public async Task<Guid> CreateAsync(IncomeTypeCreateDto dto, Guid createdBy)
	{
		var entity = new IncomeTypeMaster
		{
			IncomeTypeId = Guid.NewGuid(),
			IncomeTypeName = dto.IncomeTypeName,
			Description = dto.Description,
			IsActive = dto.IsActive,
			CreatedBy = createdBy
		};

		_db.IncomeTypes.Add(entity);
		await _db.SaveChangesAsync();

		return entity.IncomeTypeId;
	}

	public async Task UpdateAsync(Guid id, IncomeTypeUpdateDto dto, Guid updatedBy)
	{
		var entity = await _db.IncomeTypes.FirstOrDefaultAsync(x => x.IncomeTypeId == id && !x.IsDeleted);
		if (entity == null)
			throw new KeyNotFoundException("Income Type not found");

		entity.IncomeTypeName = dto.IncomeTypeName;
		entity.Description = dto.Description;
		entity.IsActive = dto.IsActive;

		entity.UpdatedBy = updatedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id, Guid deletedBy)
	{
		var entity = await _db.IncomeTypes.FirstOrDefaultAsync(x => x.IncomeTypeId == id && !x.IsDeleted);
		if (entity == null) return;

		entity.IsDeleted = true;
		entity.UpdatedBy = deletedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}
}
