using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.TripRates;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;

namespace TipperERP.Infrastructure.Services;

public class TripRateService : ITripRateService
{
	private readonly TipperErpDbContext _db;

	public TripRateService(TipperErpDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<TripRateListDto>> GetAllAsync()
	{
		return await _db.TripRates
			.Include(t => t.Customer)
			.Include(t => t.Site)
			.Include(t => t.Dumping)
			.Include(t => t.Route)
			.Include(t => t.MaterialType)
			.Where(t => !t.IsDeleted)
			.OrderBy(t => t.RateType)
			.Select(t => new TripRateListDto
			{
				RateId = t.RateId,
				CustomerId = t.CustomerId,
				CustomerName = t.Customer != null ? t.Customer.CustomerName : null,
				SiteId = t.SiteId,
				SiteName = t.Site != null ? t.Site.SiteName : null,
				DumpingId = t.DumpingId,
				DumpingName = t.Dumping != null ? t.Dumping.DumpingName : null,
				RouteId = t.RouteId,
				RouteName = t.Route != null ? t.Route.RouteName : null,
				MaterialTypeId = t.MaterialTypeId,
				MaterialTypeName = t.MaterialType != null ? t.MaterialType.MaterialName : null,

				RateType = t.RateType,
				RateValue = t.RateValue,
				Currency = t.Currency,
				EffectiveFrom = t.EffectiveFrom,
				EffectiveTo = t.EffectiveTo,
				IsActive = t.IsActive
			})
			.ToListAsync();
	}

	public async Task<TripRateListDto?> GetByIdAsync(Guid rateId)
	{
		return await _db.TripRates
			.Include(t => t.Customer)
			.Include(t => t.Site)
			.Include(t => t.Dumping)
			.Include(t => t.Route)
			.Include(t => t.MaterialType)
			.Where(t => t.RateId == rateId && !t.IsDeleted)
			.Select(t => new TripRateListDto
			{
				RateId = t.RateId,
				CustomerId = t.CustomerId,
				CustomerName = t.Customer != null ? t.Customer.CustomerName : null,
				SiteId = t.SiteId,
				SiteName = t.Site != null ? t.Site.SiteName : null,
				DumpingId = t.DumpingId,
				DumpingName = t.Dumping != null ? t.Dumping.DumpingName : null,
				RouteId = t.RouteId,
				RouteName = t.Route != null ? t.Route.RouteName : null,
				MaterialTypeId = t.MaterialTypeId,
				MaterialTypeName = t.MaterialType != null ? t.MaterialType.MaterialName : null,

				RateType = t.RateType,
				RateValue = t.RateValue,
				Currency = t.Currency,
				EffectiveFrom = t.EffectiveFrom,
				EffectiveTo = t.EffectiveTo,
				IsActive = t.IsActive
			})
			.FirstOrDefaultAsync();
	}

	public async Task<Guid> CreateAsync(TripRateCreateDto dto, Guid createdBy)
	{
		var entity = new TripRateMaster
		{
			RateId = Guid.NewGuid(),
			CustomerId = dto.CustomerId,
			SiteId = dto.SiteId,
			DumpingId = dto.DumpingId,
			RouteId = dto.RouteId,
			MaterialTypeId = dto.MaterialTypeId,
			RateType = dto.RateType,
			RateValue = dto.RateValue,
			Currency = dto.Currency,
			EffectiveFrom = dto.EffectiveFrom,
			EffectiveTo = dto.EffectiveTo,
			IsActive = dto.IsActive,
			CreatedBy = createdBy
		};

		_db.TripRates.Add(entity);
		await _db.SaveChangesAsync();

		return entity.RateId;
	}

	public async Task UpdateAsync(Guid rateId, TripRateUpdateDto dto, Guid updatedBy)
	{
		var entity = await _db.TripRates.FirstOrDefaultAsync(t => t.RateId == rateId && !t.IsDeleted);
		if (entity == null)
			throw new KeyNotFoundException("Trip rate not found");

		entity.CustomerId = dto.CustomerId;
		entity.SiteId = dto.SiteId;
		entity.DumpingId = dto.DumpingId;
		entity.RouteId = dto.RouteId;
		entity.MaterialTypeId = dto.MaterialTypeId;
		entity.RateType = dto.RateType;
		entity.RateValue = dto.RateValue;
		entity.Currency = dto.Currency;
		entity.EffectiveFrom = dto.EffectiveFrom;
		entity.EffectiveTo = dto.EffectiveTo;
		entity.IsActive = dto.IsActive;

		entity.UpdatedBy = updatedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid rateId, Guid deletedBy)
	{
		var entity = await _db.TripRates.FirstOrDefaultAsync(t => t.RateId == rateId && !t.IsDeleted);
		if (entity == null) return;

		entity.IsDeleted = true;
		entity.UpdatedBy = deletedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}
}
