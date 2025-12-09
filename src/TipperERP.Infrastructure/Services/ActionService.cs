using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.Action;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;

namespace TipperERP.Infrastructure.Services;

public class ActionService : IActionService
{
	private readonly TipperErpDbContext _db;

	public ActionService(TipperErpDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<ActionListDto>> GetAllAsync()
	{
		return await _db.Actions
			.Where(x => x.IsActive)
			.OrderBy(x => x.ActionName)
			.Select(x => new ActionListDto
			{
				ActionId = x.ActionId,
				ActionName = x.ActionName,
				Description = x.Description,
				IsActive = x.IsActive
			})
			.ToListAsync();
	}

	public async Task<ActionListDto?> GetByIdAsync(Guid id)
	{
		return await _db.Actions
			.Where(x => x.ActionId == id)
			.Select(x => new ActionListDto
			{
				ActionId = x.ActionId,
				ActionName = x.ActionName,
				Description = x.Description,
				IsActive = x.IsActive
			})
			.FirstOrDefaultAsync();
	}

	public async Task<Guid> CreateAsync(ActionCreateDto dto)
	{
		var entity = new ActionMaster
		{
			ActionId = Guid.NewGuid(),
			ActionName = dto.ActionName,
			Description = dto.Description,
			IsActive = dto.IsActive
		};

		_db.Actions.Add(entity);
		await _db.SaveChangesAsync();

		return entity.ActionId;
	}

	public async Task UpdateAsync(Guid id, ActionUpdateDto dto)
	{
		var entity = await _db.Actions.FirstOrDefaultAsync(x => x.ActionId == id);
		if (entity == null)
			throw new KeyNotFoundException("Action not found");

		entity.ActionName = dto.ActionName;
		entity.Description = dto.Description;
		entity.IsActive = dto.IsActive;

		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id)
	{
		var entity = await _db.Actions.FirstOrDefaultAsync(x => x.ActionId == id);
		if (entity == null) return;

		entity.IsActive = false;
		await _db.SaveChangesAsync();
	}
}
