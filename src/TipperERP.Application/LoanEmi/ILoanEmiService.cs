using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TipperERP.Application.LoanEmi;

public interface ILoanEmiService
{
	Task<IEnumerable<LoanEmiListDto>> GetAllAsync();
	Task<LoanEmiListDto?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(LoanEmiCreateDto dto, Guid createdBy);
	Task UpdateAsync(Guid id, LoanEmiUpdateDto dto, Guid updatedBy);
	Task DeleteAsync(Guid id, Guid deletedBy);
}
