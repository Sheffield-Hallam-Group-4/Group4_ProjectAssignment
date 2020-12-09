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
    public class EditModel : PageModel
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



            AdminRec = new Admin();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM Admin WHERE Id = @ID";

                command.Parameters.AddWithValue("@ID", id);
                Console.WriteLine("The id : " + id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    AdminRec.Id = reader.GetInt32(0);
                    AdminRec.AdminID = reader.GetString(1);
                    AdminRec.AdminName = reader.GetString(2);
                    AdminRec.AdminLastName = reader.GetString(3);
                    AdminRec.Email = reader.GetString(4);
                    AdminRec.Password = reader.GetString(5);
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

            Console.WriteLine("Admin ID : " + AdminRec.Id);
            Console.WriteLine("Admin's Admin ID : " + AdminRec.AdminID);
            Console.WriteLine("Admin First Name : " + AdminRec.AdminName);
            Console.WriteLine("Admin Last Name : " + AdminRec.AdminLastName);
            Console.WriteLine("Admin Email : " + AdminRec.Email);
            Console.WriteLine("Admin Password : " + AdminRec.Password);

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "UPDATE Admin SET AdminID = @AdminID, AdminName = @AdminName, AdminLastName = @AdminLName, Email = @Email, Password = @PSWD WHERE Id = @ID";

                command.Parameters.AddWithValue("@ID", AdminRec.Id);
                command.Parameters.AddWithValue("@AdminID", AdminRec.AdminID);
                command.Parameters.AddWithValue("@AdminName", AdminRec.AdminName);
                command.Parameters.AddWithValue("@AdminLName", AdminRec.AdminLastName);
                command.Parameters.AddWithValue("@Email", AdminRec.Email);
                command.Parameters.AddWithValue("@PSWD", AdminRec.Password);

                command.ExecuteNonQuery();
            }

            conn.Close();

            return RedirectToPage("./Index");
        }
    }
}
