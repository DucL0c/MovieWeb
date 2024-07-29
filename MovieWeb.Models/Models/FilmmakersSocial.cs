using System;
using System.Collections.Generic;

namespace MovieWeb.Models.Models;

public partial class FilmmakersSocial
{
    public int Id { get; set; }

    public int Filmmakersid { get; set; }

    public int Socialid { get; set; }

    public virtual Filmmaker Filmmakers { get; set; } = null!;

    public virtual Social Social { get; set; } = null!;
}
