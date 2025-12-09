using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.Driver;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;

namespace TipperERP.Infrastructure.Services;

public class DriverService : IDriverService
{
	private readonly TipperErpDbContext _db;

	public DriverService(TipperErpDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<DriverListDto>> GetAllAsync()
	{
		return await _db.Drivers
			.Include(d => d.Company)
			.Where(d => !d.IsDeleted)
			.OrderBy(d => d.DriverName)
			.Select(d => new DriverListDto
			{
				DriverId = d.DriverId,
				DriverCode = d.DriverCode,
				DriverName = d.DriverName,
				CompanyId = d.CompanyId,
				CompanyName = d.Company.CompanyName,
				MobileNo = d.MobileNo,
				AlternateMobileNo = d.AlternateMobileNo,
				LicenseNumber = d.LicenseNumber,
				LicenseExpiryDate = d.LicenseExpiryDate,
				JoiningDate = d.JoiningDate,
				IsActive = d.IsActive
			})
			.ToListAsync();
	}

	public async Task<DriverListDto?> GetByIdAsync(Guid id)
	{
		return await _db.Drivers
			.Include(d => d.Company)
			.Where(d => d.DriverId == id && !d.IsDeleted)
			.Select(d => new DriverListDto
			{
				DriverId = d.DriverId,
				DriverCode = d.DriverCode,
				DriverName = d.DriverName,
				CompanyId = d.CompanyId,
				CompanyName = d.Company.CompanyName,
				MobileNo = d.MobileNo,
				AlternateMobileNo = d.AlternateMobileNo,
				LicenseNumber = d.LicenseNumber,
				LicenseExpiryDate = d.LicenseExpiryDate,
				JoiningDate = d.JoiningDate,
				IsActive = d.IsActive
			})
			.FirstOrDefaultAsync();
	}

	public async Task<Guid> CreateAsync(DriverCreateDto dto, Guid createdBy)
	{
		var entity = new DriverMaster
		{
			DriverId = Guid.NewGuid(),
			CompanyId = dto.CompanyId,
			DriverCode = dto.DriverCode,
			DriverName = dto.DriverName,
			MobileNo = dto.MobileNo,
			AlternateMobileNo = dto.AlternateMobileNo,
			Address = dto.Address,
			LicenseNumber = dto.LicenseNumber,
			LicenseExpiryDate = dto.LicenseExpiryDate,
			JoiningDate = dto.JoiningDate,
			IsActive = dto.IsActive,
			CreatedBy = createdBy
		};

		_db.Drivers.Add(entity);
		await _db.SaveChangesAsync();

		return entity.DriverId;
	}

	public async Task UpdateAsync(Guid id, DriverUpdateDto dto, Guid updatedBy)
	{
		var entity = await _db.Drivers.FirstOrDefaultAsync(d => d.DriverId == id && !d.IsDeleted);
		if (entity == null)
			throw new KeyNotFoundException("Driver not found");

		entity.CompanyId = dto.CompanyId;
		entity.DriverCode = dto.DriverCode;
		entity.DriverName = dto.DriverName;
		entity.MobileNo = dto.MobileNo;
		entity.AlternateMobileNo = dto.AlternateMobileNo;
		entity.Address = dto.Address;
		entity.LicenseNumber = dto.LicenseNumber;
		entity.LicenseExpiryDate = dto.LicenseExpiryDate;
		entity.JoiningDate = dto.JoiningDate;
		entity.IsActive = dto.IsActive;

		entity.UpdatedBy = updatedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id, Guid deletedBy)
	{
		var entity = await _db.Drivers.FirstOrDefaultAsync(d => d.DriverId == id && !d.IsDeleted);
		if (entity == null) return;

		entity.IsDeleted = true;
		entity.UpdatedBy = deletedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}
}
