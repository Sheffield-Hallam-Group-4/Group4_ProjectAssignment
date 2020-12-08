using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Group_Project.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Customer ID")]
        public string CustomerID { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Last Name")]
        public string CustomerLastName { get; set; }

        [Display(Name = "Customer Email")]
        public string Email { get; set; }

        [Display(Name = "Customer Password")]
        public string Password { get; set; }

    }
}
