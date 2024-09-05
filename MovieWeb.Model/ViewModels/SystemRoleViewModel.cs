using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWeb.Model.ViewModels
{
    public class SystemRoleViewModel
    {
        public int Id { get; set; }
        [Required]
        public string NameRole { get; set; } = null!;
        [Required]
        public string RoleCode { get; set; } = null!;
        public int? ParentId { get; set; } = null!;
        public string Icon { get; set; } = null!;

    }
}
