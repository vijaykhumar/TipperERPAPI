using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TipperERP.Application.Action;

public interface IActionService
{
	Task<IEnumerable<ActionListDto>> GetAllAsync();
	Task<ActionListDto?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(ActionCreateDto dto);
	Task UpdateAsync(Guid id, ActionUpdateDto dto);
	Task DeleteAsync(Guid id);
}
