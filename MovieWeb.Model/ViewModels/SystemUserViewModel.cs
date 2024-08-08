using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWeb.Model.ViewModels
{
    public class SystemUserViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;
        [Required]
        public int SystemGroupid { get; set; }
    }
}
