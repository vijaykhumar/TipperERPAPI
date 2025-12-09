using System;

namespace TipperERP.Application.Driver;

public class DriverListDto
{
	public Guid DriverId { get; set; }
	public string DriverCode { get; set; } = string.Empty;
	public string DriverName { get; set; } = string.Empty;

	public Guid CompanyId { get; set; }
	public string? CompanyName { get; set; }

	public string? MobileNo { get; set; }
	public string? AlternateMobileNo { get; set; }
	public string? LicenseNumber { get; set; }
	public DateTime? LicenseExpiryDate { get; set; }
	public DateTime? JoiningDate { get; set; }
	public bool IsActive { get; set; }
}

public class DriverCreateDto
{
	public Guid CompanyId { get; set; }

	public string DriverCode { get; set; } = string.Empty;
	public string DriverName { get; set; } = string.Empty;

	public string? MobileNo { get; set; }
	public string? AlternateMobileNo { get; set; }
	public string? Address { get; set; }

	public string? LicenseNumber { get; set; }
	public DateTime? LicenseExpiryDate { get; set; }
	public DateTime? JoiningDate { get; set; }

	public bool IsActive { get; set; } = true;
}

public class DriverUpdateDto : DriverCreateDto
{
}
