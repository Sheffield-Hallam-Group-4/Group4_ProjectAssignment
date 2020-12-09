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

namespace Group_Project1.Pages.Customers
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Customer CustomerReg { get; set; }
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

            Console.WriteLine(CustomerReg.Email);
            Console.WriteLine(CustomerReg.Password);


            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT CustomerName, CustomerLastName, Email, Password FROM Customer WHERE Email = @Email AND Password = @PSWD";

                command.Parameters.AddWithValue("@Email", CustomerReg.Email);
                command.Parameters.AddWithValue("@PSWD", CustomerReg.Password);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CustomerReg.CustomerName = reader.GetString(0);
                    CustomerReg.CustomerLastName = reader.GetString(1);
                    CustomerReg.Email = reader.GetString(2);
                }
            }

            if (!string.IsNullOrEmpty(CustomerReg.CustomerName))
            {
                SessionID = HttpContext.Session.Id;
                HttpContext.Session.SetString("sessionID", SessionID);
                HttpContext.Session.SetString("Email", CustomerReg.Email);
                HttpContext.Session.SetString("CustomerName", CustomerReg.CustomerName);
            }
            else
            {
                Message = "Invalid Username and Password!";
                return Page();
            }
            return RedirectToPage("/Customers/Login/CustomersPage");
        }
    }
}
