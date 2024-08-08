using System;
using System.Collections.Generic;

namespace MovieWeb.Model.Models;

public partial class Filmmaker
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly BirthDay { get; set; }

    public string Address { get; set; } = null!;

    public string Image { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public DateOnly? ModifiedDate { get; set; }

    public string? ModifierBy { get; set; }

    public virtual ICollection<FilmmakersDepartment> FilmmakersDepartments { get; set; } = new List<FilmmakersDepartment>();

    public virtual ICollection<FilmmakersSocial> FilmmakersSocials { get; set; } = new List<FilmmakersSocial>();
}
