using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TipperERP.Application.TipperDocument;

public interface ITipperDocumentService
{
	Task<IEnumerable<TipperDocumentListDto>> GetAllAsync();
	Task<TipperDocumentListDto?> GetByIdAsync(Guid documentId);
	Task<Guid> CreateAsync(TipperDocumentCreateDto dto, Guid createdBy);
	Task UpdateAsync(Guid documentId, TipperDocumentUpdateDto dto, Guid updatedBy);
	Task DeleteAsync(Guid documentId, Guid deletedBy);
}
