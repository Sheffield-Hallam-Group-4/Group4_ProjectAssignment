using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Group_Project.Models;
using Group_Project1.Data;

namespace Group_Project1.Pages.Admins
{
    public class DetailsModel : PageModel
    {
        private readonly Group_Project1.Data.Group_Project1Context _context;

        public DetailsModel(Group_Project1.Data.Group_Project1Context context)
        {
            _context = context;
        }

        public Admin Admin { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Admin = await _context.Admin.FirstOrDefaultAsync(m => m.Id == id);

            if (Admin == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
