using System;
using System.Collections.Generic;

namespace MovieWeb.Model.Models;

public partial class Episode
{
    public int Id { get; set; }

    public string NameEpisodes { get; set; } = null!;

    public int Movieid { get; set; }

    public string? CreatedBy { get; set; }

    public DateOnly? CreatedDate { get; set; }

    public DateOnly? ModifiedDate { get; set; }

    public string? ModifierBy { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public virtual ICollection<Source> Sources { get; set; } = new List<Source>();

    public virtual ICollection<UserEpisode> UserEpisodes { get; set; } = new List<UserEpisode>();
}
