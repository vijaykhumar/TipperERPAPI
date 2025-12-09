using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TipperERP.Application.Financier;

public interface IFinancierService
{
	Task<IEnumerable<FinancierListDto>> GetAllAsync();
	Task<FinancierListDto?> GetByIdAsync(Guid financierId);
	Task<Guid> CreateAsync(FinancierCreateDto dto, Guid createdBy);
	Task UpdateAsync(Guid financierId, FinancierUpdateDto dto, Guid updatedBy);
	Task DeleteAsync(Guid financierId, Guid deletedBy);
}
