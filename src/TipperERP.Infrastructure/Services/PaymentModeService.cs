using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.PaymentMode;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;

namespace TipperERP.Infrastructure.Services;

public class PaymentModeService : IPaymentModeService
{
	private readonly TipperErpDbContext _db;

	public PaymentModeService(TipperErpDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<PaymentModeListDto>> GetAllAsync()
	{
		return await _db.PaymentModes
			.Where(x => !x.IsDeleted)
			.OrderBy(x => x.PaymentModeName)
			.Select(x => new PaymentModeListDto
			{
				PaymentModeId = x.PaymentModeId,
				PaymentModeName = x.PaymentModeName,
				Description = x.Description,
				IsActive = x.IsActive
			})
			.ToListAsync();
	}

	public async Task<PaymentModeListDto?> GetByIdAsync(Guid id)
	{
		return await _db.PaymentModes
			.Where(x => x.PaymentModeId == id && !x.IsDeleted)
			.Select(x => new PaymentModeListDto
			{
				PaymentModeId = x.PaymentModeId,
				PaymentModeName = x.PaymentModeName,
				Description = x.Description,
				IsActive = x.IsActive
			})
			.FirstOrDefaultAsync();
	}

	public async Task<Guid> CreateAsync(PaymentModeCreateDto dto, Guid createdBy)
	{
		var entity = new PaymentModeMaster
		{
			PaymentModeId = Guid.NewGuid(),
			PaymentModeName = dto.PaymentModeName,
			Description = dto.Description,
			IsActive = dto.IsActive,
			CreatedBy = createdBy
		};

		_db.PaymentModes.Add(entity);
		await _db.SaveChangesAsync();

		return entity.PaymentModeId;
	}

	public async Task UpdateAsync(Guid id, PaymentModeUpdateDto dto, Guid updatedBy)
	{
		var entity = await _db.PaymentModes.FirstOrDefaultAsync(x => x.PaymentModeId == id && !x.IsDeleted);
		if (entity == null)
			throw new KeyNotFoundException("Payment mode not found");

		entity.PaymentModeName = dto.PaymentModeName;
		entity.Description = dto.Description;
		entity.IsActive = dto.IsActive;
		entity.UpdatedBy = updatedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id, Guid deletedBy)
	{
		var entity = await _db.PaymentModes.FirstOrDefaultAsync(x => x.PaymentModeId == id && !x.IsDeleted);
		if (entity == null) return;

		entity.IsDeleted = true;
		entity.UpdatedBy = deletedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}
}
