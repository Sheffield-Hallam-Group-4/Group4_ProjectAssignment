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
    public class IndexModel : PageModel
    {
        public List<Customer> CustomerRec { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Customer { get; set; }

        public void OnGet()
        {

            DatabaseConnect dbstring = new DatabaseConnect(); //creating an object from the class
            string DbConnection = dbstring.DatabaseString(); //calling the method from the class
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM Customer";

                SqlDataReader reader = command.ExecuteReader(); //SqlDataReader is used to read record from a table

                CustomerRec = new List<Customer>(); //this object of list is created to populate all records from the table

                while (reader.Read())
                {
                    Customer record = new Customer(); //a local var to hold a record temporarily
                    record.Id = reader.GetInt32(0); //getting the first field from the table
                    record.CustomerID = reader.GetString(1); //getting the second field from the table
                    record.CustomerName = reader.GetString(2); //getting the third field from the table
                    record.CustomerLastName = reader.GetString(3);
                    record.Email = reader.GetString(4);

                    CustomerRec.Add(record); //adding the single record into the list
                }

                // Call Close when done reading.
                reader.Close();
            }

        }
    }
}
