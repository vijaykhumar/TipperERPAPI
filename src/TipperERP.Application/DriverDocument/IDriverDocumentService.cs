using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TipperERP.Application.DriverDocument;

public interface IDriverDocumentService
{
	Task<IEnumerable<DriverDocumentListDto>> GetAllAsync();
	Task<DriverDocumentListDto?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(DriverDocumentCreateDto dto, Guid createdBy);
	Task UpdateAsync(Guid id, DriverDocumentUpdateDto dto, Guid updatedBy);
	Task DeleteAsync(Guid id, Guid deletedBy);
}
