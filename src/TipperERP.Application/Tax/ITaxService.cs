using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TipperERP.Application.Tax;

public interface ITaxService
{
	Task<IEnumerable<TaxListDto>> GetAllAsync();
	Task<TaxListDto?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(TaxCreateDto dto, Guid createdBy);
	Task UpdateAsync(Guid id, TaxUpdateDto dto, Guid updatedBy);
	Task DeleteAsync(Guid id, Guid deletedBy);
}
