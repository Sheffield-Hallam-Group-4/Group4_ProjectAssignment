using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Group_Project.Models;

namespace Group_Project1.Data
{
    public class Group_Project1Context : DbContext
    {
        public Group_Project1Context (DbContextOptions<Group_Project1Context> options)
            : base(options)
        {
        }

        public DbSet<Group_Project.Models.Admin> Admin { get; set; }

        public DbSet<Group_Project.Models.Customer> Customer { get; set; }
    }
}
