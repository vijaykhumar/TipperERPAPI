using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TipperERP.Application.IncomeType;

public interface IIncomeTypeService
{
	Task<IEnumerable<IncomeTypeListDto>> GetAllAsync();
	Task<IncomeTypeListDto?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(IncomeTypeCreateDto dto, Guid createdBy);
	Task UpdateAsync(Guid id, IncomeTypeUpdateDto dto, Guid updatedBy);
	Task DeleteAsync(Guid id, Guid deletedBy);
}
