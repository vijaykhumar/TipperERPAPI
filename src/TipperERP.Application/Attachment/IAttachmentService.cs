using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TipperERP.Application.Attachment;

public interface IAttachmentService
{
	Task<IEnumerable<AttachmentListDto>> GetByEntityAsync(string entityType, Guid entityId);
	Task<AttachmentResponseDto> UploadAsync(string entityType, Guid entityId, IFormFile file, Guid uploadedBy);
	Task DeleteAsync(Guid attachmentId);
}
