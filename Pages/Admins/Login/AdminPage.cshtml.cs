using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project1.Pages.Admins.Login
{
    public class AdminPageModel : PageModel
    {
        public string Email;
        public const string SessionKeyName1 = "Email";

        public string AdminName;
        public const string SessionKeyName2 = "AdminName";

        public string SessionID;
        public const string SessionKeyName3 = "sessionID";


        public IActionResult OnGet()
        {
            Email = HttpContext.Session.GetString(SessionKeyName1);
            AdminName = HttpContext.Session.GetString(SessionKeyName2);
            SessionID = HttpContext.Session.GetString(SessionKeyName3);

            if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(AdminName) && string.IsNullOrEmpty(SessionID))
            {
                return RedirectToPage("/Admins/Login");
            }
            return Page();
        }
    }
}
