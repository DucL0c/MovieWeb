using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieWeb.Model.ViewModels
{
    public class SystemGroupRoleViewModel
    {
        public int Id { get; set; }
        [Required]
        public int SystemRoleid { get; set; }
        [Required]
        public int SystemGroupid { get; set; }
    }
}
