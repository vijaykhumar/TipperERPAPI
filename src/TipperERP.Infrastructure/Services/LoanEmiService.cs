using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.LoanEmi;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;

namespace TipperERP.Infrastructure.Services;

public class LoanEmiService : ILoanEmiService
{
	private readonly TipperErpDbContext _db;

	public LoanEmiService(TipperErpDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<LoanEmiListDto>> GetAllAsync()
	{
		return await _db.LoanEmis
			.Include(x => x.Tipper)
			.Include(x => x.Financier)
			.Where(x => !x.IsDeleted)
			.OrderBy(x => x.EmiStartDate)
			.Select(x => new LoanEmiListDto
			{
				EmiId = x.EmiId,
				TipperId = x.TipperId,
				TipperNumber = x.Tipper.TipperNumber,
				FinancierId = x.FinancierId,
				FinancierName = x.Financier.FinancierName,
				EmiStartDate = x.EmiStartDate,
				EmiEndDate = x.EmiEndDate,
				MonthlyEmiAmount = x.MonthlyEmiAmount,
				TenureMonths = x.TenureMonths,
				Remarks = x.Remarks,
				IsActive = x.IsActive
			})
			.ToListAsync();
	}

	public async Task<LoanEmiListDto?> GetByIdAsync(Guid id)
	{
		return await _db.LoanEmis
			.Include(x => x.Tipper)
			.Include(x => x.Financier)
			.Where(x => x.EmiId == id && !x.IsDeleted)
			.Select(x => new LoanEmiListDto
			{
				EmiId = x.EmiId,
				TipperId = x.TipperId,
				TipperNumber = x.Tipper.TipperNumber,
				FinancierId = x.FinancierId,
				FinancierName = x.Financier.FinancierName,
				EmiStartDate = x.EmiStartDate,
				EmiEndDate = x.EmiEndDate,
				MonthlyEmiAmount = x.MonthlyEmiAmount,
				TenureMonths = x.TenureMonths,
				Remarks = x.Remarks,
				IsActive = x.IsActive
			})
			.FirstOrDefaultAsync();
	}

	public async Task<Guid> CreateAsync(LoanEmiCreateDto dto, Guid createdBy)
	{
		var entity = new LoanEmiMaster
		{
			EmiId = Guid.NewGuid(),
			TipperId = dto.TipperId,
			FinancierId = dto.FinancierId,
			EmiStartDate = dto.EmiStartDate,
			EmiEndDate = dto.EmiEndDate,
			MonthlyEmiAmount = dto.MonthlyEmiAmount,
			TenureMonths = dto.TenureMonths,
			Remarks = dto.Remarks,
			IsActive = dto.IsActive,
			CreatedBy = createdBy
		};

		_db.LoanEmis.Add(entity);
		await _db.SaveChangesAsync();

		return entity.EmiId;
	}

	public async Task UpdateAsync(Guid id, LoanEmiUpdateDto dto, Guid updatedBy)
	{
		var entity = await _db.LoanEmis.FirstOrDefaultAsync(x => x.EmiId == id && !x.IsDeleted);
		if (entity == null)
			throw new KeyNotFoundException("Loan EMI record not found");

		entity.TipperId = dto.TipperId;
		entity.FinancierId = dto.FinancierId;
		entity.EmiStartDate = dto.EmiStartDate;
		entity.EmiEndDate = dto.EmiEndDate;
		entity.MonthlyEmiAmount = dto.MonthlyEmiAmount;
		entity.TenureMonths = dto.TenureMonths;
		entity.Remarks = dto.Remarks;
		entity.IsActive = dto.IsActive;
		entity.UpdatedBy = updatedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id, Guid deletedBy)
	{
		var entity = await _db.LoanEmis.FirstOrDefaultAsync(x => x.EmiId == id && !x.IsDeleted);
		if (entity == null) return;

		entity.IsDeleted = true;
		entity.UpdatedBy = deletedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}
}
