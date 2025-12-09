using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TipperERP.Application.AuditLog;

public interface IAuditLogService
{
	Task<IEnumerable<AuditLogListDto>> GetAllAsync();
	Task<IEnumerable<AuditLogListDto>> GetByEntityAsync(string entityName, Guid entityId);
	Task<Guid> CreateAsync(AuditLogCreateDto dto, Guid performedBy);
}
