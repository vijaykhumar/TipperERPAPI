using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TipperERP.Application.DocumentType;

public interface IDocumentTypeService
{
	Task<IEnumerable<DocumentTypeListDto>> GetAllAsync();
	Task<DocumentTypeListDto?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(DocumentTypeCreateDto dto, Guid createdBy);
	Task UpdateAsync(Guid id, DocumentTypeUpdateDto dto, Guid updatedBy);
	Task DeleteAsync(Guid id, Guid deletedBy);
}
