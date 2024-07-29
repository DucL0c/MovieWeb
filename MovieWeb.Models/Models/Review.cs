using System;
using System.Collections.Generic;

namespace MovieWeb.Models.Models;

public partial class Review
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateOnly Time { get; set; }

    public int Userid { get; set; }

    public int Movieid { get; set; }

    public int? Reviewid { get; set; }

    public virtual ICollection<Review> InverseReviewNavigation { get; set; } = new List<Review>();

    public virtual Movie Movie { get; set; } = null!;

    public virtual Review? ReviewNavigation { get; set; }

    public virtual User User { get; set; } = null!;
}
