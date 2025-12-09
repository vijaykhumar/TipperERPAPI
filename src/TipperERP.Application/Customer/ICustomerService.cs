using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TipperERP.Domain.Entities;

namespace TipperERP.Application.Customer;

public interface ICustomerService
{
	Task<IEnumerable<CustomerListDto>> GetAllAsync();
	Task<CustomerListDto?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(CustomerCreateDto dto, Guid createdBy);
	Task UpdateAsync(Guid id, CustomerUpdateDto dto, Guid updatedBy);
	Task DeleteAsync(Guid id, Guid deletedBy);
}

