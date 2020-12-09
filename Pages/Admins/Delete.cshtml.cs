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

namespace Group_Project1.Pages.Admins
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Admin AdminRec { get; set; }
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
                command.CommandText = "SELECT * FROM Admin WHERE Id = @ID";
                command.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = command.ExecuteReader();
                AdminRec = new Admin();
                while (reader.Read())
                {
                    AdminRec.Id = reader.GetInt32(0);
                    AdminRec.AdminID = reader.GetString(1);
                    AdminRec.AdminName = reader.GetString(2);
                    AdminRec.AdminLastName = reader.GetString(3);
                    AdminRec.Email = reader.GetString(4);
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
                command.CommandText = "DELETE Admin WHERE Id = @ID";
                command.Parameters.AddWithValue("@ID", AdminRec.Id);
                command.ExecuteNonQuery();
            }
            conn.Close();
            return RedirectToPage("./Index");
        }
    }
}