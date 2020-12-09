using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Group_Project.Models;
using Group_Project1.Data;
using System.Data.SqlClient;
using Login_Session.Pages.DatabaseConnection;

namespace Group_Project1.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Customer CustomerRec { get; set; }
        public IActionResult OnGet(int? id)
        {
            DatabaseConnect dbstring = new DatabaseConnect(); //creating an object from the class
            string DbConnection = dbstring.DatabaseString(); //calling the method from the class
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM Customer WHERE Id = @ID";
                command.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = command.ExecuteReader();
                CustomerRec = new Customer();
                while (reader.Read())
                {
                    CustomerRec.Id = reader.GetInt32(0);
                    CustomerRec.CustomerID = reader.GetString(1);
                    CustomerRec.CustomerName = reader.GetString(2);
                    CustomerRec.CustomerLastName = reader.GetString(3);
                    CustomerRec.Email = reader.GetString(4);
                }

            }

            conn.Close();

            return Page();
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
                command.CommandText = "DELETE Customer WHERE Id = @ID";
                command.Parameters.AddWithValue("@ID", CustomerRec.Id);
                command.ExecuteNonQuery();
            }

            conn.Close();
            return RedirectToPage("./Index");
        }
    }
}
