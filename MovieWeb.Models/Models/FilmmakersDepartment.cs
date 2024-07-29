using System;
using System.Collections.Generic;

namespace MovieWeb.Models.Models;

public partial class FilmmakersDepartment
{
    public int Id { get; set; }

    public int Filmmakersid { get; set; }

    public int Departmentid { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual Filmmaker Filmmakers { get; set; } = null!;

    public virtual ICollection<MovieFilmmaker> MovieFilmmakers { get; set; } = new List<MovieFilmmaker>();
}
