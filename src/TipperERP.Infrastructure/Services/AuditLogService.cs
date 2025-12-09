using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.AuditLog;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;

namespace TipperERP.Infrastructure.Services;

public class AuditLogService : IAuditLogService
{
	private readonly TipperErpDbContext _db;

	public AuditLogService(TipperErpDbContext db)
	{
		_db = db;
	}

	public async Task<IEnumerable<AuditLogListDto>> GetAllAsync()
	{
		return await _db.AuditLogs
			.OrderByDescending(x => x.PerformedDate)
			.Select(x => new AuditLogListDto
			{
				AuditId = x.AuditId,
				EntityName = x.EntityName,
				EntityId = x.EntityId,
				Action = x.Action,
				OldValueJson = x.OldValueJson,
				NewValueJson = x.NewValueJson,
				PerformedBy = x.PerformedBy,
				PerformedDate = x.PerformedDate
			})
			.ToListAsync();
	}

	public async Task<IEnumerable<AuditLogListDto>> GetByEntityAsync(string entityName, Guid entityId)
	{
		return await _db.AuditLogs
			.Where(x => x.EntityName == entityName && x.EntityId == entityId)
			.OrderByDescending(x => x.PerformedDate)
			.Select(x => new AuditLogListDto
			{
				AuditId = x.AuditId,
				EntityName = x.EntityName,
				EntityId = x.EntityId,
				Action = x.Action,
				OldValueJson = x.OldValueJson,
				NewValueJson = x.NewValueJson,
				PerformedBy = x.PerformedBy,
				PerformedDate = x.PerformedDate
			})
			.ToListAsync();
	}

	public async Task<Guid> CreateAsync(AuditLogCreateDto dto, Guid performedBy)
	{
		var log = new AuditLogMaster
		{
			AuditId = Guid.NewGuid(),
			EntityName = dto.EntityName,
			EntityId = dto.EntityId,
			Action = dto.Action,
			OldValueJson = dto.OldValueJson,
			NewValueJson = dto.NewValueJson,
			PerformedBy = performedBy,
			PerformedDate = DateTime.UtcNow
		};

		_db.AuditLogs.Add(log);
		await _db.SaveChangesAsync();

		return log.AuditId;
	}
}
