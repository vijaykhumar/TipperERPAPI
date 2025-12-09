using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TipperERP.Application.Tipper;

public interface ITipperService
{
	Task<IEnumerable<TipperListDto>> GetAllAsync();
	Task<TipperListDto?> GetByIdAsync(Guid tipperId);
	Task<Guid> CreateAsync(TipperCreateDto dto, Guid createdBy);
	Task UpdateAsync(Guid tipperId, TipperUpdateDto dto, Guid updatedBy);
	Task DeleteAsync(Guid tipperId, Guid deletedBy);
}
