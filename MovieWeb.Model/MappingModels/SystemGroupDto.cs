﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWeb.Model.MappingModels
{
    public class SystemGroupDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string GroupCode { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? ModifierBy { get; set; }
    }
}
