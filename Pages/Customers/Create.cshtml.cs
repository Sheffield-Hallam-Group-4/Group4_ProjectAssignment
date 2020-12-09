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


namespace Group_Project1.Pages.Customers
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Customer Customer { get; set; }

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
                command.CommandText = @"INSERT INTO Customer (CustomerID, CustomerName, CustomerLastName, Email, Password) VALUES (@CID, @CName, @CLName, @Email, @PSWD)";

                command.Parameters.AddWithValue("@CID", Customer.CustomerID);
                command.Parameters.AddWithValue("@CName", Customer.CustomerName);
                command.Parameters.AddWithValue("@CLName", Customer.CustomerLastName);
                command.Parameters.AddWithValue("@Email", Customer.Email);
                command.Parameters.AddWithValue("@PSWD", Customer.Password);

                Console.WriteLine(Customer.CustomerID);
                Console.WriteLine(Customer.CustomerName);
                Console.WriteLine(Customer.CustomerLastName);
                Console.WriteLine(Customer.Email);
                Console.WriteLine(Customer.Password);

                command.ExecuteNonQuery();
            }
            return RedirectToPage("./Index");
        }
    }
}