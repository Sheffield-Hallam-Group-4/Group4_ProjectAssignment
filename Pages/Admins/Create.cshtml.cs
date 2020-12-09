using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Group_Project.Models;
using Group_Project1.Data;
using System.Data.SqlClient;
using Login_Session.Pages.DatabaseConnection;
using System.Data.Common;

namespace Group_Project1.Pages.Admins
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Admin Admin { get; set; }
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

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"INSERT INTO Admin (AdminID, AdminName, AdminLastName, Email, Password) VALUES (@AID, @AName, @ALName, @Email, @PSWD)";

                command.Parameters.AddWithValue("@AID", Admin.AdminID);
                command.Parameters.AddWithValue("@AName", Admin.AdminName);
                command.Parameters.AddWithValue("@ALName", Admin.AdminLastName);
                command.Parameters.AddWithValue("@Email", Admin.Email);
                command.Parameters.AddWithValue("@PSWD", Admin.Password);

                Console.WriteLine(Admin.AdminID);
                Console.WriteLine(Admin.AdminName);
                Console.WriteLine(Admin.AdminLastName);
                Console.WriteLine(Admin.Email);
                Console.WriteLine(Admin.Password);

                command.ExecuteNonQuery();
            }
            return RedirectToPage("./Index");
        }
    }
}