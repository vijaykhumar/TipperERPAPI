using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.ExpenseType;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;

namespace TipperERP.Infrastructure.Services;

public class ExpenseTypeService : IExpenseTypeService
{
	private readonly TipperErpDbContext _db;

	public ExpenseTypeService(TipperErpDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<ExpenseTypeListDto>> GetAllAsync()
	{
		return await _db.ExpenseTypes
			.Where(x => !x.IsDeleted)
			.OrderBy(x => x.ExpenseTypeName)
			.Select(x => new ExpenseTypeListDto
			{
				ExpenseTypeId = x.ExpenseTypeId,
				ExpenseTypeName = x.ExpenseTypeName,
				Category = x.Category,
				Description = x.Description,
				IsActive = x.IsActive
			})
			.ToListAsync();
	}

	public async Task<ExpenseTypeListDto?> GetByIdAsync(Guid id)
	{
		return await _db.ExpenseTypes
			.Where(x => x.ExpenseTypeId == id && !x.IsDeleted)
			.Select(x => new ExpenseTypeListDto
			{
				ExpenseTypeId = x.ExpenseTypeId,
				ExpenseTypeName = x.ExpenseTypeName,
				Category = x.Category,
				Description = x.Description,
				IsActive = x.IsActive
			})
			.FirstOrDefaultAsync();
	}

	public async Task<Guid> CreateAsync(ExpenseTypeCreateDto dto, Guid createdBy)
	{
		var entity = new ExpenseTypeMaster
		{
			ExpenseTypeId = Guid.NewGuid(),
			ExpenseTypeName = dto.ExpenseTypeName,
			Category = dto.Category,
			Description = dto.Description,
			IsActive = dto.IsActive,
			CreatedBy = createdBy
		};

		_db.ExpenseTypes.Add(entity);
		await _db.SaveChangesAsync();

		return entity.ExpenseTypeId;
	}

	public async Task UpdateAsync(Guid id, ExpenseTypeUpdateDto dto, Guid updatedBy)
	{
		var entity = await _db.ExpenseTypes.FirstOrDefaultAsync(x => x.ExpenseTypeId == id && !x.IsDeleted);
		if (entity == null)
			throw new KeyNotFoundException("Expense Type not found");

		entity.ExpenseTypeName = dto.ExpenseTypeName;
		entity.Category = dto.Category;
		entity.Description = dto.Description;
		entity.IsActive = dto.IsActive;

		entity.UpdatedBy = updatedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}

	public async Task DeleteAsync(Guid id, Guid deletedBy)
	{
		var entity = await _db.ExpenseTypes.FirstOrDefaultAsync(x => x.ExpenseTypeId == id && !x.IsDeleted);
		if (entity == null) return;

		entity.IsDeleted = true;
		entity.UpdatedBy = deletedBy;
		entity.UpdatedDate = DateTime.UtcNow;

		await _db.SaveChangesAsync();
	}
}
