using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TipperERP.Domain.Entities;

namespace TipperERP.Application.Dumping;

public interface IDumpingService
{
	Task<IEnumerable<DumpingListDto>> GetAllAsync();
	Task<DumpingListDto?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(DumpingCreateDto dto, Guid createdBy);
	Task UpdateAsync(Guid id, DumpingUpdateDto dto, Guid updatedBy);
	Task DeleteAsync(Guid id, Guid deletedBy);
}

