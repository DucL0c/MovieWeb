using System;
using System.Collections.Generic;

namespace MovieWeb.Model.Models;

public partial class Genre
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public DateOnly? ModifiedDate { get; set; }

    public string? ModifierBy { get; set; }

    public virtual ICollection<GenreMovie> GenreMovies { get; set; } = new List<GenreMovie>();
}
