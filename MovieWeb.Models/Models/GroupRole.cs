using System;
using System.Collections.Generic;

namespace MovieWeb.Models.Models;

public partial class GroupRole
{
    public int Id { get; set; }

    public int SystemRoleid { get; set; }

    public int SystemGroupid { get; set; }

    public virtual SystemGroup SystemGroup { get; set; } = null!;

    public virtual SystemRole SystemRole { get; set; } = null!;
}
