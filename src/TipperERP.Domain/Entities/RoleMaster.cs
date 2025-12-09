using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TipperERP.Domain.Entities;

public class RoleMaster : BaseEntity
{
	[Key]
	[Column("RoleId")]
	public Guid RoleId { get; set; }
	public string RoleName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<UserMaster> Users { get; set; } = new List<UserMaster>();
}
