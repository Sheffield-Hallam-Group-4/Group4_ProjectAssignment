using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Group_Project.Models;
using Group_Project1.Data;
using System.Data.SqlClient;
using Login_Session.Pages.DatabaseConnection;


namespace Group_Project1.Pages.Customers
{
    public class EditModel : PageModel
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



            CustomerRec = new Customer();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM Customer WHERE Id = @ID";

                command.Parameters.AddWithValue("@ID", id);
                Console.WriteLine("The id : " + id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CustomerRec.Id = reader.GetInt32(0);
                    CustomerRec.CustomerID = reader.GetString(1);
                    CustomerRec.CustomerName = reader.GetString(2);
                    CustomerRec.CustomerLastName = reader.GetString(3);
                    CustomerRec.Email = reader.GetString(4);
                    CustomerRec.Password = reader.GetString(5);
                }


            }

            conn.Close();

            return Page();

        }


        public IActionResult OnPost()
        {
            DatabaseConnect dbstring = new DatabaseConnect();
            string DbConnection = dbstring.DatabaseString();
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            Console.WriteLine("Customer ID : " + CustomerRec.Id);
            Console.WriteLine("Cusomter's Customer ID : " + CustomerRec.CustomerID);
            Console.WriteLine("Customer First Name : " + CustomerRec.CustomerName);
            Console.WriteLine("Customer Last Name : " + CustomerRec.CustomerLastName);
            Console.WriteLine("Customer Email : " + CustomerRec.Email);
            Console.WriteLine("Customer Password : " + CustomerRec.Password);

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "UPDATE Customer SET CustomerID = @CID, CustomerName = @CName, CustomerLastName = @CLName, Email = @Email, Password = @PSWD WHERE Id = @ID";

                command.Parameters.AddWithValue("@ID", CustomerRec.Id);
                command.Parameters.AddWithValue("@CID", CustomerRec.CustomerID);
                command.Parameters.AddWithValue("@CName", CustomerRec.CustomerName);
                command.Parameters.AddWithValue("@CLName", CustomerRec.CustomerLastName);
                command.Parameters.AddWithValue("@Email", CustomerRec.Email);
                command.Parameters.AddWithValue("@PSWD", CustomerRec.Password);

                command.ExecuteNonQuery();
            }

            conn.Close();

            return RedirectToPage("./Index");
        }
    }
}
