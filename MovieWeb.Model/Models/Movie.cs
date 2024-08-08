using System;
using System.Collections.Generic;

namespace MovieWeb.Model.Models;

public partial class Movie
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string EnglishName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Type { get; set; } = null!;

    public DateOnly YearOfProduct { get; set; }

    public TimeOnly Duration { get; set; }

    public string Overview { get; set; } = null!;

    public float Score { get; set; }

    public string Image { get; set; } = null!;

    public string Trailer { get; set; } = null!;

    public int NumberOfViews { get; set; }

    public string IsLock { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public DateOnly? ModifiedDate { get; set; }

    public string? ModifierBy { get; set; }

    public virtual ICollection<Episode> Episodes { get; set; } = new List<Episode>();

    public virtual ICollection<GenreMovie> GenreMovies { get; set; } = new List<GenreMovie>();

    public virtual ICollection<MovieFilmmaker> MovieFilmmakers { get; set; } = new List<MovieFilmmaker>();

    public virtual ICollection<PointStar> PointStars { get; set; } = new List<PointStar>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<WatchList> WatchLists { get; set; } = new List<WatchList>();
}
