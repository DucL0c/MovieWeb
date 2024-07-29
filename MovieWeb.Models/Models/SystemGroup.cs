using System;
using System.Collections.Generic;

namespace MovieWeb.Models.Models;

public partial class SystemGroup
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string GroupCode { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public DateOnly? ModifiedDate { get; set; }

    public string? ModifierBy { get; set; }

    public virtual ICollection<GroupRole> GroupRoles { get; set; } = new List<GroupRole>();

    public virtual ICollection<SystemUser> SystemUsers { get; set; } = new List<SystemUser>();
}
