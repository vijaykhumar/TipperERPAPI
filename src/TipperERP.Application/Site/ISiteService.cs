using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TipperERP.Application.Customer;
using TipperERP.Domain.Entities;

namespace TipperERP.Application.Site;

public interface ISiteService
{
	Task<IEnumerable<SiteListDto>> GetAllAsync();
	Task<SiteListDto?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(SiteCreateDto dto, Guid createdBy);
	Task UpdateAsync(Guid id, SiteUpdateDto dto, Guid updatedBy);
	Task DeleteAsync(Guid id, Guid deletedBy);
}


