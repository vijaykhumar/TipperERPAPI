using System;

namespace TipperERP.Domain.Entities;

public class AttachmentMaster
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
