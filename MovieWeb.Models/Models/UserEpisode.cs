using System;
using System.Collections.Generic;

namespace MovieWeb.Models.Models;

public partial class UserEpisode
{
    public int Id { get; set; }

    public DateOnly LastViewTime { get; set; }

    public int Userid { get; set; }

    public int Episodesid { get; set; }

    public virtual Episode Episodes { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
