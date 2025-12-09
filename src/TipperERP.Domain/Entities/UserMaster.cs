using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TipperERP.Domain.Entities;

public class UserMaster : BaseEntity
{
    [Key]
    [Column("UserId")]
    public Guid UserId { get; set; }
	public Guid? CompanyId { get; set; }
    public Guid? BranchId { get; set; }
    public Guid RoleId { get; set; }

    public string FullName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? MobileNo { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? LastLogin { get; set; }

    public CompanyMaster? Company { get; set; }
    public BranchMaster? Branch { get; set; }
    public RoleMaster Role { get; set; } = null!;
}
