using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.InsuranceCompany;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;

namespace TipperERP.Infrastructure.Services;

public class InsuranceCompanyService : IInsuranceCompanyService
{
	private readonly TipperErpDbContext _db;

	public InsuranceCompanyService(TipperErpDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<InsuranceCompanyListDto>> GetAllAsync()
	{
		return await _db.InsuranceCompanies
			.Where(x => !x.IsDeleted)
			.OrderBy(x => x.CompanyName)
			.Select(x => new InsuranceCompanyListDto
			{
				InsuranceCompanyId = x.InsuranceCompanyId,
				CompanyName = x.CompanyName,
				ContactNumber = x.ContactNumber,
				Email = x.Email,
				IsActive = x.IsActive
			})
			.ToListAsync();
	}

	public async Task<InsuranceCompanyListDto?> GetByIdAsync(Guid id)
	{
		return await _db.InsuranceCompanies
			.Where(x => x.InsuranceCompanyId == id && !x.IsDeleted)
			.Select(x => new InsuranceCompanyListDto
			{
				InsuranceCompanyId = x.InsuranceCompanyId,
				CompanyName = x.CompanyName,
				ContactNumber = x.ContactNumber,
				Email = x.Email,
				IsActive = x.IsActive
			})
			.FirstOrDefaultAsync();
	}

	public async Task<Guid> CreateAsync(InsuranceCompanyCreateDto dto, Guid createdBy)
	{
		var entity = new InsuranceCompanyMaster
		{
			InsuranceCompanyId = Guid.NewGuid(),
			CompanyName = dto.CompanyName,
			ContactNumber = dto.ContactNumber,
			Email = dto.Email,
			IsActive = dto.IsActive,
			CreatedBy = createdBy
		};

		_db.InsuranceCompanies.Add(entity);
		await _db.SaveChangesAsync();

		return entity.InsuranceCompanyId;
	}

	public async Task UpdateAsync(Guid id, InsuranceCompanyUpdateDto dto, Guid updatedBy)
	{
		var entity = await _db.InsuranceCompanies.FirstOrDefaultAsync(x => x.InsuranceCompanyId == id && !x.IsDeleted);
		if (entity == null)
			throw new KeyNotFoundException("Insurance company not found");

		entity.CompanyName = dto.CompanyName;
		entity.ContactNumber = dto.ContactNumber;
		entity.Email = dto.Email;
		entity.IsActive = dto.IsActive;
		entity.UpdatedBy = updatedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id, Guid deletedBy)
	{
		var entity = await _db.InsuranceCompanies.FirstOrDefaultAsync(x => x.InsuranceCompanyId == id && !x.IsDeleted);
		if (entity == null) return;

		entity.IsDeleted = true;
		entity.UpdatedBy = deletedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}
}
