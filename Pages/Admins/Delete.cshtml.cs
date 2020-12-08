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
    public class DeleteModel : PageModel
    {
        private readonly Group_Project1.Data.Group_Project1Context _context;

        public DeleteModel(Group_Project1.Data.Group_Project1Context context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Admin = await _context.Admin.FindAsync(id);

            if (Admin != null)
            {
                _context.Admin.Remove(Admin);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
