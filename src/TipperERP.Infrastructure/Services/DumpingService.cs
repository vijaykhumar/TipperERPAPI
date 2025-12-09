using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.Dumping;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;

namespace TipperERP.Infrastructure.Services;

public class DumpingService : IDumpingService
{
	private readonly TipperErpDbContext _db;

	public DumpingService(TipperErpDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<DumpingListDto>> GetAllAsync()
	{
		return await _db.DumpingSites
			.Include(d => d.Customer)
			.Where(d => !d.IsDeleted)
			.OrderBy(d => d.DumpingName)
			.Select(d => new DumpingListDto
			{
				DumpingId = d.DumpingId,
				CustomerId = d.CustomerId,
				CustomerName = d.Customer != null ? d.Customer.CustomerName : null,
				DumpingName = d.DumpingName,
				Address = d.Address,
				IsActive = d.IsActive
			})
			.ToListAsync();
	}

	public async Task<DumpingListDto?> GetByIdAsync(Guid id)
	{
		return await _db.DumpingSites
			.Include(d => d.Customer)
			.Where(d => d.DumpingId == id && !d.IsDeleted)
			.Select(d => new DumpingListDto
			{
				DumpingId = d.DumpingId,
				CustomerId = d.CustomerId,
				CustomerName = d.Customer != null ? d.Customer.CustomerName : null,
				DumpingName = d.DumpingName,
				Address = d.Address,
				IsActive = d.IsActive
			})
			.FirstOrDefaultAsync();
	}

	public async Task<Guid> CreateAsync(DumpingCreateDto dto, Guid createdBy)
	{
		var entity = new DumpingMaster
		{
			DumpingId = Guid.NewGuid(),
			CustomerId = dto.CustomerId,
			DumpingName = dto.DumpingName,
			Address = dto.Address,
			IsActive = dto.IsActive,
			CreatedBy = createdBy
		};

		_db.DumpingSites.Add(entity);
		await _db.SaveChangesAsync();

		return entity.DumpingId;
	}

	public async Task UpdateAsync(Guid id, DumpingUpdateDto dto, Guid updatedBy)
	{
		var entity = await _db.DumpingSites.FirstOrDefaultAsync(d => d.DumpingId == id && !d.IsDeleted);
		if (entity == null) throw new KeyNotFoundException("Dumping site not found");

		entity.CustomerId = dto.CustomerId;
		entity.DumpingName = dto.DumpingName;
		entity.Address = dto.Address;
		entity.IsActive = dto.IsActive;
		entity.UpdatedBy = updatedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id, Guid deletedBy)
	{
		var entity = await _db.DumpingSites.FirstOrDefaultAsync(d => d.DumpingId == id && !d.IsDeleted);
		if (entity == null) return;

		entity.IsDeleted = true;
		entity.UpdatedBy = deletedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}
}
