using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TipperERP.Application.Driver;

public interface IDriverService
{
	Task<IEnumerable<DriverListDto>> GetAllAsync();
	Task<DriverListDto?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(DriverCreateDto dto, Guid createdBy);
	Task UpdateAsync(Guid id, DriverUpdateDto dto, Guid updatedBy);
	Task DeleteAsync(Guid id, Guid deletedBy);
}
