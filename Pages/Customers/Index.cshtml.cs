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
    public class IndexModel : PageModel
    {
        private readonly Group_Project1Context _context;

        public IndexModel(Group_Project1.Data.Group_Project1Context context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get;set; }

    }
}
