using System;

namespace TipperERP.Application.Attachment;

public class AttachmentListDto
{
	public Guid AttachmentId { get; set; }
	public string EntityType { get; set; } = string.Empty;
	public Guid EntityId { get; set; }
	public string FilePath { get; set; } = string.Empty;
	public string FileName { get; set; } = string.Empty;
	public string? FileType { get; set; }
	public Guid UploadedBy { get; set; }
	public DateTime UploadedDate { get; set; }
}

public class AttachmentResponseDto
{
	public Guid AttachmentId { get; set; }
	public string FileUrl { get; set; } = string.Empty;
	public string FileName { get; set; } = string.Empty;
	public string? FileType { get; set; }
}
