using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieWeb.Model.Models;

public partial class SystemRole
{
    public int Id { get; set; }

    public string NameRole { get; set; } = null!;

    public string RoleCode { get; set; } = null!;
    public int? ParentId { get; set; }
    public string? Description { get; set; } = null!;
    public string? Icon { get; set; } = null!;
    public string? Link { get; set; } = null!;

    public string? ActiveLink { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifierBy { get; set; }

    public virtual ICollection<GroupRole> GroupRoles { get; set; } = new List<GroupRole>();
}
