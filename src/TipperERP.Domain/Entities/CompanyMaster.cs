using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TipperERP.Domain.Entities;

public class CompanyMaster : BaseEntity
{
	[Key]
	[Column("CompanyId")]
	public Guid CompanyId { get; set; }
	public string CompanyCode { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? GstRegistrationNo { get; set; }
    public string? LogoPath { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<BranchMaster> Branches { get; set; } = new List<BranchMaster>();
    public ICollection<UserMaster> Users { get; set; } = new List<UserMaster>();
}
