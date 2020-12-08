using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Models
{
    public class Admin
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Admin ID")]
        public string AdminID { get; set; }

        [Required]
        [Display(Name = "Admin Name")]
        public string AdminName { get; set; }

        [Display(Name = "Admin Last Name")]
        public string AdminLastName { get; set; }

        [Display(Name = "Admin Email")]
        public string Email { get; set; }

        [Display(Name = "Admin Password")]
        public string Password { get; set; }
    }
}