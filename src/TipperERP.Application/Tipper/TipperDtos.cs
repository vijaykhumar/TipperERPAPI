using System;

namespace TipperERP.Application.Tipper;

public class TipperListDto
{
	public Guid TipperId { get; set; }
	public string TipperNumber { get; set; } = string.Empty;

	public Guid CompanyId { get; set; }
	public string? CompanyName { get; set; }

	public Guid? BranchId { get; set; }
	public string? BranchName { get; set; }

	public Guid? FinancierId { get; set; }
	public string? FinancierName { get; set; }

	public string? RegistrationNumber { get; set; }
	public string? Make { get; set; }
	public string? Model { get; set; }
	public int? YearOfManufacture { get; set; }
	public bool IsActive { get; set; }
}

public class TipperCreateDto
{
	public Guid CompanyId { get; set; }
	public Guid? BranchId { get; set; }
	public Guid? FinancierId { get; set; }

	public string TipperNumber { get; set; } = string.Empty;
	public string? RegistrationNumber { get; set; }
	public string? ChassisNumber { get; set; }
	public string? EngineNumber { get; set; }
	public string? Make { get; set; }
	public string? Model { get; set; }
	public int? YearOfManufacture { get; set; }

	public string? OwnershipType { get; set; }
	public DateTime? PurchaseDate { get; set; }
	public decimal? PurchaseAmount { get; set; }

	public bool IsActive { get; set; } = true;
}

public class TipperUpdateDto : TipperCreateDto
{
}
