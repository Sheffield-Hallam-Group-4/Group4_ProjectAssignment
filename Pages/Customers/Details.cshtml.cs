using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Group_Project.Models;
using Group_Project1.Data;

namespace Group_Project1.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly Group_Project1.Data.Group_Project1Context _context;

        public DetailsModel(Group_Project1.Data.Group_Project1Context context)
        {
            _context = context;
        }

        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer = await _context.Customer.FirstOrDefaultAsync(m => m.Id == id);

            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
