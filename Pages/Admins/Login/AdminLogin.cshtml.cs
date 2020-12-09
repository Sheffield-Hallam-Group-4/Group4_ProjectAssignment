using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Group_Project.Models;
using Login_Session.Pages.DatabaseConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project1.Pages.Admins.Login
{
    public class AdminLoginModel : PageModel
    {
        [BindProperty]
        public Admin AdminReg { get; set; }
        public string Message { get; set; }

        public string SessionID;

        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {

            DatabaseConnect dbstring = new DatabaseConnect(); //creating an object from the class
            string DbConnection = dbstring.DatabaseString(); //calling the method from the class
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            Console.WriteLine(AdminReg.Email);
            Console.WriteLine(AdminReg.Password);


            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT AdminName, AdminLastName, Email, Password FROM Admin WHERE Email = @Email AND Password = @PSWD";

                command.Parameters.AddWithValue("@Email", AdminReg.Email);
                command.Parameters.AddWithValue("@PSWD", AdminReg.Password);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    AdminReg.AdminName = reader.GetString(0);
                    AdminReg.AdminLastName = reader.GetString(1);
                    AdminReg.Email = reader.GetString(2);
                }
            }

            if (!string.IsNullOrEmpty(AdminReg.AdminName))
            {
                SessionID = HttpContext.Session.Id;
                HttpContext.Session.SetString("sessionID", SessionID);
                HttpContext.Session.SetString("Email", AdminReg.Email);
                HttpContext.Session.SetString("AdminName", AdminReg.AdminName);
            }
            else
            {
                Message = "Invalid Username and Password!";
                return Page();
            }
            return RedirectToPage("/Admins/Login/AdminPage");
        }
    }
}
