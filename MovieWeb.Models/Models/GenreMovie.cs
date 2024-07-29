using System;
using System.Collections.Generic;

namespace MovieWeb.Models.Models;

public partial class GenreMovie
{
    public int Id { get; set; }

    public int Genreid { get; set; }

    public int Movieid { get; set; }

    public virtual Genre Genre { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
