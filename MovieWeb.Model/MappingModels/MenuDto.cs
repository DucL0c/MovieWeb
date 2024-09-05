using MovieWeb.Model.Models;
using System.Collections.Generic;

namespace MovieWeb.Model.MappingModels
{
    public class MenuDto
    {
        public int Id { get; set; }

        public string? MenuName { get; set; }

        public int? ParentId { get; set; }

        public string? Icon { get; set; }

        public string? Link { get; set; }

        public string? ActiveLink { get; set; }

        public List<MenuDto>? Childrens { get; set; }
    }
}