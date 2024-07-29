using System;
using System.Collections.Generic;

namespace MovieWeb.Models.Models;

public partial class SystemRole
{
    public int Id { get; set; }

    public string NameRole { get; set; } = null!;

    public string RoleCode { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public DateOnly? ModifiedDate { get; set; }

    public string? ModifierBy { get; set; }

    public virtual ICollection<GroupRole> GroupRoles { get; set; } = new List<GroupRole>();
}
