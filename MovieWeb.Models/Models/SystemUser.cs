using System;
using System.Collections.Generic;

namespace MovieWeb.Models.Models;

public partial class SystemUser
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public int Password { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public DateOnly? ModifiedDate { get; set; }

    public string? ModifierBy { get; set; }

    public int SystemGroupid { get; set; }

    public virtual SystemGroup SystemGroup { get; set; } = null!;
}
