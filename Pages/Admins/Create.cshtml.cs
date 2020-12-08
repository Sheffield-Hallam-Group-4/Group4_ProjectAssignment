using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Group_Project.Models;
using Group_Project1.Data;

namespace Group_Project1.Pages.Admins
{
    public class CreateModel : PageModel
    {
        private readonly Group_Project1.Data.Group_Project1Context _context;

        public CreateModel(Group_Project1.Data.Group_Project1Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Admin Admin { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Admin.Add(Admin);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}