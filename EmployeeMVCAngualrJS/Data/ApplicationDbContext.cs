using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeMVCAngualrJS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=ApplicationDbContext")
        {
        }

        public DbSet<Models.Gender> Genders { get; set; }

        public DbSet<Models.Employee> Employees { get; set; }
    }
}
