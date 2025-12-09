using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TipperERP.Application.TripRates;

public interface ITripRateService
{
	Task<IEnumerable<TripRateListDto>> GetAllAsync();
	Task<TripRateListDto?> GetByIdAsync(Guid rateId);
	Task<Guid> CreateAsync(TripRateCreateDto dto, Guid createdBy);
	Task UpdateAsync(Guid rateId, TripRateUpdateDto dto, Guid updatedBy);
	Task DeleteAsync(Guid rateId, Guid deletedBy);
}
