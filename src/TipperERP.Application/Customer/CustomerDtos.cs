using System;

namespace TipperERP.Application.Customer;

public class CustomerListDto
{
	public Guid CustomerId { get; set; }
	public string CustomerCode { get; set; } = string.Empty;
	public string CustomerName { get; set; } = string.Empty;
	public string? ContactPerson { get; set; }
	public string? ContactNo { get; set; }
	public string PaymentTerms { get; set; }
	public decimal CreditLimit { get; set; }
	public string GstRegistrationNo { get; set; }	
	public bool IsActive { get; set; }
}

public class CustomerCreateDto
{
	public Guid CompanyId { get; set; }
	public Guid? BranchId { get; set; }
	public string CustomerCode { get; set; } = string.Empty;
	public string CustomerName { get; set; } = string.Empty;
	public string? AddressLine1 { get; set; }
	public string? AddressLine2 { get; set; }
	public string? City { get; set; }
	public string? State { get; set; }
	public string? Country { get; set; }
	public string? PostalCode { get; set; }
	public string? ContactPerson { get; set; }
	public string? ContactNo { get; set; }
	public string? Email { get; set; }
	public string PaymentTerms { get; set; }	
	public decimal CreditLimit { get; set; }
	public string GstRegistrationNo { get; set; }
	public bool IsActive { get; set; } = true;
	public bool IsDeleted { get; set; } = true;
}

public class CustomerUpdateDto : CustomerCreateDto
{
}

