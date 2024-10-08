﻿using System;
using System.Collections.Generic;

namespace MovieWeb.Model.Models;

public partial class PointStar
{
    public int Id { get; set; }

    public int Userid { get; set; }

    public int Movieid { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
