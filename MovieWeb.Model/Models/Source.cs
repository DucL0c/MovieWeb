using System;
using System.Collections.Generic;

namespace MovieWeb.Model.Models;

public partial class Source
{
    public int Id { get; set; }

    public string SourceFilm { get; set; } = null!;

    public string Quality { get; set; } = null!;

    public int Episodeid { get; set; }

    public string? CreatedBy { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public DateOnly? ModifiedDate { get; set; }

    public string? ModifierBy { get; set; }

    public virtual Episode Episode { get; set; } = null!;
}
