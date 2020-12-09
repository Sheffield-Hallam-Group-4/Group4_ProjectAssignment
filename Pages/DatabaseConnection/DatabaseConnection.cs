using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login_Session.Pages.DatabaseConnection
{
    public class DatabaseConnect
    {
        public string DatabaseString()
        {
            string DbString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\micha\Source\Repos\Group-Project1\Data\Group_Project1Context-2de9e534-39ac-4a0c-95f5-098bb869f1df.mdf;Integrated Security=True";
            return DbString;
        }
    }
}