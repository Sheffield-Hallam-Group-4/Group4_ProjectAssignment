using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Login_Session.Pages.DatabaseConnection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Group_Project1.Pages.UploadFile
{

    public class UploadFile : PageModel
    {
        [BindProperty]
        public IFormFile File { get; set; }

        [BindProperty]
        public UploadFile FileRec { get; set; }
        public string Name { get; private set; }

        public readonly IHostingEnvironment _env;

        public UploadFile(IHostingEnvironment env)
        {
            _env = env;
        }

        public IActionResult OnPost()
        {
            var FileToUpload = Path.Combine(_env.WebRootPath, "Files", File.FileName);//this variable consists of file path
            Console.WriteLine("File Name : " + FileToUpload);

            using (var FStream = new FileStream(FileToUpload, FileMode.Create))
            {
                File.CopyTo(FStream);//copy the file into FStream variable
            }

            DatabaseConnect DBCon = new DatabaseConnect();
            string DbString = DBCon.DatabaseString();
            SqlConnection conn = new SqlConnection(DbString);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"INSERT StudentFile (StudentName, FileName) VALUES (@StdName, @FName)";
                command.Parameters.AddWithValue("@StdName", FileRec.Name);
                command.Parameters.AddWithValue("@FName", File.FileName);
                Console.WriteLine("File name : " + FileRec.Name);
                Console.WriteLine("File name : " + File.FileName);
                command.ExecuteNonQuery();
            }

            return RedirectToPage("/index");
        }





        public void OnGet()
        {
        }

    }
}