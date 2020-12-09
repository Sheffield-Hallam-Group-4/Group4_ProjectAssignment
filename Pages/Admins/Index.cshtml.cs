using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Group_Project.Models;
using Group_Project1.Data;
using Login_Session.Pages.DatabaseConnection;
using System.Data.SqlClient;

namespace Group_Project1.Pages.Admins
{
    public class IndexModel : PageModel
    {
        public List<Admin> AdminRec { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Admin { get; set; }

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
                command.CommandText = @"SELECT * FROM Admin";

                SqlDataReader reader = command.ExecuteReader(); //SqlDataReader is used to read record from a table

                AdminRec = new List<Admin>(); //this object of list is created to populate all records from the table

                while (reader.Read())
                {
                    Admin record = new Admin
                    {
                        Id = reader.GetInt32(0), //getting the first field from the table
                        AdminID = reader.GetString(1), //getting the second field from the table
                        AdminName = reader.GetString(2), //getting the third field from the table
                        AdminLastName = reader.GetString(3),
                        Email = reader.GetString(4)
                    }; //a local var to hold a record temporarily

                    AdminRec.Add(record); //adding the single record into the list
                }

                // Call Close when done reading.
                reader.Close();
            }
        }
    }
}