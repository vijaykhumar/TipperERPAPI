using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.Route;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;

namespace TipperERP.Infrastructure.Services;

public class RouteService : IRouteService
{
	private readonly TipperErpDbContext _db;

	public RouteService(TipperErpDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<RouteListDto>> GetAllAsync()
	{
		return await _db.Routes
			.Include(r => r.Customer)
			.Include(r => r.Site)
			.Include(r => r.Dumping)
			.Where(r => !r.IsDeleted)
			.OrderBy(r => r.RouteName)
			.Select(r => new RouteListDto
			{
				RouteId = r.RouteId,
				CustomerId = r.CustomerId,
				CustomerName = r.Customer != null ? r.Customer.CustomerName : null,
				SiteId = r.SiteId,
				SiteName = r.Site != null ? r.Site.SiteName : null,
				DumpingId = r.DumpingId,
				DumpingName = r.Dumping != null ? r.Dumping.DumpingName : null,
				RouteName = r.RouteName,
				ApproxDistanceKm = r.ApproxDistanceKm,
				IsActive = r.IsActive
			})
			.ToListAsync();
	}

	public async Task<RouteListDto?> GetByIdAsync(Guid id)
	{
		return await _db.Routes
			.Include(r => r.Customer)
			.Include(r => r.Site)
			.Include(r => r.Dumping)
			.Where(r => r.RouteId == id && !r.IsDeleted)
			.Select(r => new RouteListDto
			{
				RouteId = r.RouteId,
				CustomerId = r.CustomerId,
				CustomerName = r.Customer != null ? r.Customer.CustomerName : null,
				SiteId = r.SiteId,
				SiteName = r.Site != null ? r.Site.SiteName : null,
				DumpingId = r.DumpingId,
				DumpingName = r.Dumping != null ? r.Dumping.DumpingName : null,
				RouteName = r.RouteName,
				ApproxDistanceKm = r.ApproxDistanceKm,
				IsActive = r.IsActive
			})
			.FirstOrDefaultAsync();
	}

	public async Task<Guid> CreateAsync(RouteCreateDto dto, Guid createdBy)
	{
		var entity = new RouteMaster
		{
			RouteId = Guid.NewGuid(),
			CustomerId = dto.CustomerId,
			SiteId = dto.SiteId,
			DumpingId = dto.DumpingId,
			RouteName = dto.RouteName,
			ApproxDistanceKm = dto.ApproxDistanceKm,
			IsActive = dto.IsActive,
			CreatedBy = createdBy
		};

		_db.Routes.Add(entity);
		await _db.SaveChangesAsync();
		return entity.RouteId;
	}

	public async Task UpdateAsync(Guid id, RouteUpdateDto dto, Guid updatedBy)
	{
		var entity = await _db.Routes.FirstOrDefaultAsync(r => r.RouteId == id && !r.IsDeleted);
		if (entity == null) throw new KeyNotFoundException("Route not found");

		entity.CustomerId = dto.CustomerId;
		entity.SiteId = dto.SiteId;
		entity.DumpingId = dto.DumpingId;
		entity.RouteName = dto.RouteName;
		entity.ApproxDistanceKm = dto.ApproxDistanceKm;
		entity.IsActive = dto.IsActive;
		entity.UpdatedBy = updatedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id, Guid deletedBy)
	{
		var entity = await _db.Routes.FirstOrDefaultAsync(r => r.RouteId == id && !r.IsDeleted);
		if (entity == null) return;

		entity.IsDeleted = true;
		entity.UpdatedBy = deletedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}
}
