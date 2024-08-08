using System;
using System.Collections.Generic;

namespace MovieWeb.Model.Models;

public partial class MovieFilmmaker
{
    public int Id { get; set; }

    public string NameInMovie { get; set; } = null!;

    public int FilmmakersDepartmentid { get; set; }

    public int Movieid { get; set; }

    public virtual FilmmakersDepartment FilmmakersDepartment { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
