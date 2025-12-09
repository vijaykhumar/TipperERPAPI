using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.Tax;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;

namespace TipperERP.Infrastructure.Services;

public class TaxService : ITaxService
{
	private readonly TipperErpDbContext _db;

	public TaxService(TipperErpDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<TaxListDto>> GetAllAsync()
	{
		return await _db.Taxes
			.Where(x => !x.IsDeleted)
			.OrderBy(x => x.TaxName)
			.Select(x => new TaxListDto
			{
				TaxId = x.TaxId,
				TaxName = x.TaxName,
				TaxPercentage = x.TaxPercentage,
				EffectiveFrom = x.EffectiveFrom,
				EffectiveTo = x.EffectiveTo,
				Country = x.Country,
				IsActive = x.IsActive
			})
			.ToListAsync();
	}

	public async Task<TaxListDto?> GetByIdAsync(Guid id)
	{
		return await _db.Taxes
			.Where(x => x.TaxId == id && !x.IsDeleted)
			.Select(x => new TaxListDto
			{
				TaxId = x.TaxId,
				TaxName = x.TaxName,
				TaxPercentage = x.TaxPercentage,
				EffectiveFrom = x.EffectiveFrom,
				EffectiveTo = x.EffectiveTo,
				Country = x.Country,
				IsActive = x.IsActive
			})
			.FirstOrDefaultAsync();
	}

	public async Task<Guid> CreateAsync(TaxCreateDto dto, Guid createdBy)
	{
		var entity = new TaxMaster
		{
			TaxId = Guid.NewGuid(),
			TaxName = dto.TaxName,
			TaxPercentage = dto.TaxPercentage,
			EffectiveFrom = dto.EffectiveFrom,
			EffectiveTo = dto.EffectiveTo,
			Country = dto.Country,
			IsActive = dto.IsActive,
			CreatedBy = createdBy
		};

		_db.Taxes.Add(entity);
		await _db.SaveChangesAsync();

		return entity.TaxId;
	}

	public async Task UpdateAsync(Guid id, TaxUpdateDto dto, Guid updatedBy)
	{
		var entity = await _db.Taxes.FirstOrDefaultAsync(x => x.TaxId == id && !x.IsDeleted);
		if (entity == null)
			throw new KeyNotFoundException("Tax record not found");

		entity.TaxName = dto.TaxName;
		entity.TaxPercentage = dto.TaxPercentage;
		entity.EffectiveFrom = dto.EffectiveFrom;
		entity.EffectiveTo = dto.EffectiveTo;
		entity.Country = dto.Country;
		entity.IsActive = dto.IsActive;

		entity.UpdatedBy = updatedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id, Guid deletedBy)
	{
		var entity = await _db.Taxes.FirstOrDefaultAsync(x => x.TaxId == id && !x.IsDeleted);
		if (entity == null) return;

		entity.IsDeleted = true;
		entity.UpdatedBy = deletedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}
}
