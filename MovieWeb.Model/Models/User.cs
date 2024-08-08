using System;
using System.Collections.Generic;

namespace MovieWeb.Model.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string IsPay { get; set; } = null!;

    public DateTime TimeEnd { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifierBy { get; set; }

    public virtual ICollection<PointStar> PointStars { get; set; } = new List<PointStar>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<UserEpisode> UserEpisodes { get; set; } = new List<UserEpisode>();

    public virtual ICollection<WatchList> WatchLists { get; set; } = new List<WatchList>();
}
