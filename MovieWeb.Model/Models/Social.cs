using System;
using System.Collections.Generic;

namespace MovieWeb.Model.Models;

public partial class Social
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Link { get; set; } = null!;

    public virtual ICollection<FilmmakersSocial> FilmmakersSocials { get; set; } = new List<FilmmakersSocial>();
}
