using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace TipperERP.Application.MaterialType;

public interface IMaterialTypeService
{
	Task<IEnumerable<MaterialTypeListDto>> GetAllAsync();
	Task<MaterialTypeListDto?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(MaterialTypeCreateDto dto, Guid createdBy);
	Task UpdateAsync(Guid id, MaterialTypeUpdateDto dto, Guid updatedBy);
	Task DeleteAsync(Guid id, Guid deletedBy);
}
