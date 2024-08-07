﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWeb.Model.ViewModels
{
    public class SystemGroupViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string GroupCode { get; set; } = null!;

        public string Description { get; set; } = null!;

    }
}
