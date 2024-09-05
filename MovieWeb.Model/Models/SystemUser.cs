using System;
using System.Collections.Generic;

namespace MovieWeb.Model.Models;

public partial class SystemUser
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;
    public string? Image { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifierBy { get; set; }

    public int SystemGroupid { get; set; }

    public virtual SystemGroup SystemGroup { get; set; } = null!;
}
