using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TipperERP.Application.PaymentMode;

public interface IPaymentModeService
{
	Task<IEnumerable<PaymentModeListDto>> GetAllAsync();
	Task<PaymentModeListDto?> GetByIdAsync(Guid id);
	Task<Guid> CreateAsync(PaymentModeCreateDto dto, Guid createdBy);
	Task UpdateAsync(Guid id, PaymentModeUpdateDto dto, Guid updatedBy);
	Task DeleteAsync(Guid id, Guid deletedBy);
}
