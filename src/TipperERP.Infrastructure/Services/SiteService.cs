using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.Site;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;

namespace TipperERP.Infrastructure.Services;

public class SiteService : ISiteService
{
	private readonly TipperErpDbContext _db;

	public SiteService(TipperErpDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<SiteListDto>> GetAllAsync()
	{
		return await _db.Sites
			.Include(s => s.Customer)
			.Where(s => !s.IsDeleted)
			.OrderBy(s => s.SiteName)
			.Select(s => new SiteListDto
			{
				SiteId = s.SiteId,
				CustomerId = s.CustomerId,
				CustomerName = s.Customer.CustomerName,
				SiteName = s.SiteName,
				Address = s.Address,
				IsActive = s.IsActive
			})
			.ToListAsync();
	}

	public async Task<SiteListDto?> GetByIdAsync(Guid id)
	{
		return await _db.Sites
			.Include(s => s.Customer)
			.Where(s => s.SiteId == id && !s.IsDeleted)
			.Select(s => new SiteListDto
			{
				SiteId = s.SiteId,
				CustomerId = s.CustomerId,
				CustomerName = s.Customer.CustomerName,
				SiteName = s.SiteName,
				Address = s.Address,
				IsActive = s.IsActive
			})
			.FirstOrDefaultAsync();
	}

	public async Task<Guid> CreateAsync(SiteCreateDto dto, Guid createdBy)
	{
		var entity = new SiteMaster
		{
			SiteId = Guid.NewGuid(),
			CustomerId = dto.CustomerId,
			SiteName = dto.SiteName,
			Address = dto.Address,
			IsActive = dto.IsActive,
			CreatedBy = createdBy
		};

		_db.Sites.Add(entity);
		await _db.SaveChangesAsync();

		return entity.SiteId;
	}

	public async Task UpdateAsync(Guid id, SiteUpdateDto dto, Guid updatedBy)
	{
		var entity = await _db.Sites.FirstOrDefaultAsync(s => s.SiteId == id && !s.IsDeleted);
		if (entity == null) throw new KeyNotFoundException("Site not found");

		entity.CustomerId = dto.CustomerId;
		entity.SiteName = dto.SiteName;
		entity.Address = dto.Address;
		entity.IsActive = dto.IsActive;
		entity.UpdatedBy = updatedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id, Guid deletedBy)
	{
		var entity = await _db.Sites.FirstOrDefaultAsync(s => s.SiteId == id && !s.IsDeleted);
		if (entity == null) return;

		entity.IsDeleted = true;
		entity.UpdatedBy = deletedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}
}
