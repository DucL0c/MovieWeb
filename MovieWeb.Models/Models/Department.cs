using System;
using System.Collections.Generic;

namespace MovieWeb.Models.Models;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public DateOnly? ModifiedDate { get; set; }

    public string? ModifierBy { get; set; }

    public virtual ICollection<FilmmakersDepartment> FilmmakersDepartments { get; set; } = new List<FilmmakersDepartment>();
}
