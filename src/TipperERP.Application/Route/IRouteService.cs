using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TipperERP.Domain.Entities;

namespace TipperERP.Application.Route;

public interface IRouteService
{
	Task<IEnumerable<RouteListDto>> GetAllAsync();
	Task<RouteListDto?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(RouteCreateDto dto, Guid createdBy);
	Task UpdateAsync(Guid id, RouteUpdateDto dto, Guid updatedBy);
	Task DeleteAsync(Guid id, Guid deletedBy);
}

