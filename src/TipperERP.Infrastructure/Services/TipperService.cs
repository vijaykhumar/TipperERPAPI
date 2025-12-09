using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.Tipper;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;

namespace TipperERP.Infrastructure.Services;

public class TipperService : ITipperService
{
	private readonly TipperErpDbContext _db;

	public TipperService(TipperErpDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<TipperListDto>> GetAllAsync()
	{
		return await _db.Tippers
			.Include(t => t.Company)
			.Include(t => t.Branch)
			.Include(t => t.Financier)
			.Where(t => !t.IsDeleted)
			.OrderBy(t => t.TipperNumber)
			.Select(t => new TipperListDto
			{
				TipperId = t.TipperId,
				TipperNumber = t.TipperNumber,
				CompanyId = t.CompanyId,
				CompanyName = t.Company.CompanyName,
				BranchId = t.BranchId,
				BranchName = t.Branch != null ? t.Branch.BranchName : null,
				FinancierId = t.FinancierId,
				FinancierName = t.Financier != null ? t.Financier.FinancierName : null,
				RegistrationNumber = t.RegistrationNumber,
				Make = t.Make,
				Model = t.Model,
				YearOfManufacture = t.YearOfManufacture,
				IsActive = t.IsActive
			})
			.ToListAsync();
	}

	public async Task<TipperListDto?> GetByIdAsync(Guid tipperId)
	{
		return await _db.Tippers
			.Include(t => t.Company)
			.Include(t => t.Branch)
			.Include(t => t.Financier)
			.Where(t => t.TipperId == tipperId && !t.IsDeleted)
			.Select(t => new TipperListDto
			{
				TipperId = t.TipperId,
				TipperNumber = t.TipperNumber,
				CompanyId = t.CompanyId,
				CompanyName = t.Company.CompanyName,
				BranchId = t.BranchId,
				BranchName = t.Branch != null ? t.Branch.BranchName : null,
				FinancierId = t.FinancierId,
				FinancierName = t.Financier != null ? t.Financier.FinancierName : null,
				RegistrationNumber = t.RegistrationNumber,
				Make = t.Make,
				Model = t.Model,
				YearOfManufacture = t.YearOfManufacture,
				IsActive = t.IsActive
			})
			.FirstOrDefaultAsync();
	}

	public async Task<Guid> CreateAsync(TipperCreateDto dto, Guid createdBy)
	{
		var entity = new TipperMaster
		{
			TipperId = Guid.NewGuid(),
			CompanyId = dto.CompanyId,
			BranchId = dto.BranchId,
			FinancierId = dto.FinancierId,
			TipperNumber = dto.TipperNumber,
			RegistrationNumber = dto.RegistrationNumber,
			ChassisNumber = dto.ChassisNumber,
			EngineNumber = dto.EngineNumber,
			Make = dto.Make,
			Model = dto.Model,
			YearOfManufacture = dto.YearOfManufacture,
			OwnershipType = dto.OwnershipType,
			PurchaseDate = dto.PurchaseDate,
			PurchaseAmount = dto.PurchaseAmount,
			IsActive = dto.IsActive,
			CreatedBy = createdBy
		};

		_db.Tippers.Add(entity);
		await _db.SaveChangesAsync();
		return entity.TipperId;
	}

	public async Task UpdateAsync(Guid tipperId, TipperUpdateDto dto, Guid updatedBy)
	{
		var entity = await _db.Tippers.FirstOrDefaultAsync(t => t.TipperId == tipperId && !t.IsDeleted);
		if (entity == null)
			throw new KeyNotFoundException("Tipper not found");

		entity.CompanyId = dto.CompanyId;
		entity.BranchId = dto.BranchId;
		entity.FinancierId = dto.FinancierId;
		entity.TipperNumber = dto.TipperNumber;
		entity.RegistrationNumber = dto.RegistrationNumber;
		entity.ChassisNumber = dto.ChassisNumber;
		entity.EngineNumber = dto.EngineNumber;
		entity.Make = dto.Make;
		entity.Model = dto.Model;
		entity.YearOfManufacture = dto.YearOfManufacture;
		entity.OwnershipType = dto.OwnershipType;
		entity.PurchaseDate = dto.PurchaseDate;
		entity.PurchaseAmount = dto.PurchaseAmount;
		entity.IsActive = dto.IsActive;

		entity.UpdatedBy = updatedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid tipperId, Guid deletedBy)
	{
		var entity = await _db.Tippers.FirstOrDefaultAsync(t => t.TipperId == tipperId && !t.IsDeleted);
		if (entity == null) return;

		entity.IsDeleted = true;
		entity.UpdatedBy = deletedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}
}
