using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TipperERP.Application.ExpenseType;

public interface IExpenseTypeService
{
	Task<IEnumerable<ExpenseTypeListDto>> GetAllAsync();
	Task<ExpenseTypeListDto?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(ExpenseTypeCreateDto dto, Guid createdBy);
	Task UpdateAsync(Guid id, ExpenseTypeUpdateDto dto, Guid updatedBy);
	Task DeleteAsync(Guid id, Guid deletedBy);
}
