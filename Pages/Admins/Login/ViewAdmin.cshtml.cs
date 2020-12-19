using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.IO;
using Login_Session.Pages.DatabaseConnection;
using Microsoft.AspNetCore.Hosting;
using MigraDocCore.DocumentObjectModel;
using MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes;
using MigraDocCore.DocumentObjectModel.Tables;
using MigraDocCore.Rendering;
using PdfSharpCore.Utils;
using SixLabors.ImageSharp.PixelFormats;
using Group_Project.Models;

namespace Group_Project1.Pages.Admins.Login
{
    public class ViewAdminModel : PageModel
    {
        [BindProperty]
        public List<Admin> Admin { get; set; }

        public string Email;
        public const string SessionKeyName1 = "Email";

        public string AdminName;
        public const string SessionKeyName2 = "AdminName";

        public string SessionID;
        public const string SessionKeyName3 = "sessionID";


        public IActionResult OnGet(string pdf)
        {
            Email = HttpContext.Session.GetString(SessionKeyName1);
            AdminName = HttpContext.Session.GetString(SessionKeyName2);
            SessionID = HttpContext.Session.GetString(SessionKeyName3);

            if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(AdminName) && string.IsNullOrEmpty(SessionID))
            {
                return RedirectToPage("/Customers/Login");
            }

            DatabaseConnect dbstring = new DatabaseConnect(); //creating an object from the class
            string DbConnection = dbstring.DatabaseString(); //calling the method from the class
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM Admin";

                var reader = command.ExecuteReader();

                Admin = new List<Admin>();
                while (reader.Read())
                {
                    Admin Row = new Admin(); //each record found from the table
                    Row.Id = reader.GetInt32(0);
                    Row.AdminID = reader.GetString(1);
                    Row.AdminName = reader.GetString(2);
                    Row.Email = reader.GetString(4); // We dont get the password. The role field is in the 5th position
                    Admin.Add(Row);
                }

            }

            //PDF code here!
            if (pdf == "1")
            {
                //Create an object for pdf document
                Document doc = new Document();
                Section sec = doc.AddSection();
                Paragraph para = sec.AddParagraph();

                para.Format.Font.Name = "Arial";
                para.Format.Font.Size = 14;
                para.Format.Font.Color = Color.FromCmyk(0, 0, 0, 100); //black colour
                para.AddFormattedText("List of Users", TextFormat.Bold);
                para.Format.SpaceAfter = "1.0cm";



                //Table
                Table tab = new Table();
                tab.Borders.Width = 0.75;
                tab.TopPadding = 5;
                tab.BottomPadding = 5;

                //Column
                Column col = tab.AddColumn(Unit.FromCentimeter(1.5));
                col.Format.Alignment = ParagraphAlignment.Justify;
                tab.AddColumn(Unit.FromCentimeter(3));
                tab.AddColumn(Unit.FromCentimeter(3));
                tab.AddColumn(Unit.FromCentimeter(2));

                //Row
                Row row = tab.AddRow();
                row.Shading.Color = Colors.Coral;

                //Cell for header
                Cell cell = new Cell();
                cell = row.Cells[0];
                cell.AddParagraph("ID");
                cell = row.Cells[1];
                cell.AddParagraph("Admin ID");
                cell = row.Cells[2];
                cell.AddParagraph("Admin Name");
                cell = row.Cells[3];
                cell.AddParagraph("Email");


                //Add data to table 
                for (int i = 0; i < Admin.Count; i++)
                {
                    row = tab.AddRow();
                    cell = row.Cells[0];
                    cell.AddParagraph(Convert.ToString(i + 1));
                    cell = row.Cells[1];
                    cell.AddParagraph(Admin[i].AdminID);
                    cell = row.Cells[2];
                    cell.AddParagraph(Admin[i].AdminName);
                    cell = row.Cells[3];
                    cell.AddParagraph(Admin[i].Email);
                }

                tab.SetEdge(0, 0, 4, (Admin.Count + 1), Edge.Box, BorderStyle.Single, 1.5, Colors.Black);
                sec.Add(tab);

                //Rendering
                PdfDocumentRenderer pdfRen = new PdfDocumentRenderer();
                pdfRen.Document = doc;
                pdfRen.RenderDocument();

                //Create a memory stream
                MemoryStream stream = new MemoryStream();
                pdfRen.PdfDocument.Save(stream); //saving the file into the stream

                Response.Headers.Add("content-disposition", new[] { "inline; filename = ListofAdmin.pdf" });
                return File(stream, "application/pdf");

            }

            return Page();
        }
    }
}

