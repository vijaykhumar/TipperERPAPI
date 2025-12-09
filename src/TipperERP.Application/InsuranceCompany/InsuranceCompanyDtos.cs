using System;

namespace TipperERP.Application.InsuranceCompany;

public class InsuranceCompanyListDto
{
	public Guid InsuranceCompanyId { get; set; }
	public string CompanyName { get; set; } = string.Empty;
	public string? ContactNumber { get; set; }
	public string? Email { get; set; }
	public bool IsActive { get; set; }
}

public class InsuranceCompanyCreateDto
{
	public string CompanyName { get; set; } = string.Empty;
	public string? ContactNumber { get; set; }
	public string? Email { get; set; }
	public bool IsActive { get; set; } = true;
}

public class InsuranceCompanyUpdateDto : InsuranceCompanyCreateDto
{
}
