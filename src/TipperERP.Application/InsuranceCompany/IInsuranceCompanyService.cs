using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TipperERP.Application.InsuranceCompany;

public interface IInsuranceCompanyService
{
	Task<IEnumerable<InsuranceCompanyListDto>> GetAllAsync();
	Task<InsuranceCompanyListDto?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(InsuranceCompanyCreateDto dto, Guid createdBy);
	Task UpdateAsync(Guid id, InsuranceCompanyUpdateDto dto, Guid updatedBy);
	Task DeleteAsync(Guid id, Guid deletedBy);
}
