using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TipperERP.Application.Attachment;
using TipperERP.Domain.Entities;
using TipperERP.Infrastructure.Data;

namespace TipperERP.Infrastructure.Services;

public class AttachmentService : IAttachmentService
{
	private readonly TipperErpDbContext _db;
	private readonly FileStorageService _fileStorage;

	public AttachmentService(TipperErpDbContext db, FileStorageService fileStorage)
	{
		_db = db;
		_fileStorage = fileStorage;
	}

	public async Task<IEnumerable<AttachmentListDto>> GetByEntityAsync(string entityType, Guid entityId)
	{
		return await _db.Attachments
			.Where(x => x.EntityType == entityType && x.EntityId == entityId)
			.OrderByDescending(x => x.UploadedDate)
			.Select(x => new AttachmentListDto
			{
				AttachmentId = x.AttachmentId,
				EntityType = x.EntityType,
				EntityId = x.EntityId,
				FilePath = x.FilePath,
				FileName = x.FileName,
				FileType = x.FileType,
				UploadedBy = x.UploadedBy,
				UploadedDate = x.UploadedDate
			})
			.ToListAsync();
	}

	public async Task<AttachmentResponseDto> UploadAsync(string entityType, Guid entityId, IFormFile file, Guid uploadedBy)
	{
		string filePath = "";// await _fileStorage.SaveFileAsync(file);

		var attachment = new AttachmentMaster
		{
			AttachmentId = Guid.NewGuid(),
			EntityType = entityType,
			EntityId = entityId,
			FilePath = filePath,
			FileName = file.FileName,
			FileType = file.ContentType,
			UploadedBy = uploadedBy,
			UploadedDate = DateTime.UtcNow
		};

		_db.Attachments.Add(attachment);
		await _db.SaveChangesAsync();

		return new AttachmentResponseDto
		{
			AttachmentId = attachment.AttachmentId,
			FileUrl = filePath,
			FileName = attachment.FileName,
			FileType = attachment.FileType
		};
	}

	public async Task DeleteAsync(Guid attachmentId)
	{
		var entity = await _db.Attachments.FirstOrDefaultAsync(x => x.AttachmentId == attachmentId);
		if (entity == null) return;

		_db.Attachments.Remove(entity);
		await _db.SaveChangesAsync();
	}
}
