using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.Financier;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;

namespace TipperERP.Infrastructure.Services;

public class FinancierService : IFinancierService
{
	private readonly TipperErpDbContext _db;

	public FinancierService(TipperErpDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<FinancierListDto>> GetAllAsync()
	{
		return await _db.Financiers
			.Where(f => !f.IsDeleted)
			.OrderBy(f => f.FinancierName)
			.Select(f => new FinancierListDto
			{
				FinancierId = f.FinancierId,
				FinancierName = f.FinancierName,
				ContactPerson = f.ContactPerson,
				Phone = f.Phone,
				Email = f.Email,
				Address = f.Address,
				IsActive = f.IsActive
			})
			.ToListAsync();
	}

	public async Task<FinancierListDto?> GetByIdAsync(Guid financierId)
	{
		return await _db.Financiers
			.Where(f => f.FinancierId == financierId && !f.IsDeleted)
			.Select(f => new FinancierListDto
			{
				FinancierId = f.FinancierId,
				FinancierName = f.FinancierName,
				ContactPerson = f.ContactPerson,
				Phone = f.Phone,
				Email = f.Email,
				Address = f.Address,
				IsActive = f.IsActive
			})
			.FirstOrDefaultAsync();
	}

	public async Task<Guid> CreateAsync(FinancierCreateDto dto, Guid createdBy)
	{
		var entity = new FinancierMaster
		{
			FinancierId = Guid.NewGuid(),
			FinancierName = dto.FinancierName,
			ContactPerson = dto.ContactPerson,
			Phone = dto.Phone,
			Email = dto.Email,
			Address = dto.Address,
			IsActive = dto.IsActive,
			CreatedBy = createdBy
		};

		_db.Financiers.Add(entity);
		await _db.SaveChangesAsync();
		return entity.FinancierId;
	}

	public async Task UpdateAsync(Guid financierId, FinancierUpdateDto dto, Guid updatedBy)
	{
		var entity = await _db.Financiers.FirstOrDefaultAsync(f => f.FinancierId == financierId && !f.IsDeleted);
		if (entity == null)
			throw new KeyNotFoundException("Financier not found");

		entity.FinancierName = dto.FinancierName;
		entity.ContactPerson = dto.ContactPerson;
		entity.Phone = dto.Phone;
		entity.Email = dto.Email;
		entity.Address = dto.Address;
		entity.IsActive = dto.IsActive;

		entity.UpdatedBy = updatedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid financierId, Guid deletedBy)
	{
		var entity = await _db.Financiers.FirstOrDefaultAsync(f => f.FinancierId == financierId && !f.IsDeleted);
		if (entity == null) return;

		entity.IsDeleted = true;
		entity.UpdatedBy = deletedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}
}
