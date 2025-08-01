using Manage.DAL.Entities;
using Manage.DAL.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text; 
using System.Threading.Tasks;

namespace Manage.DAL.Data.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> // IdentityDbContext<ApplicationUser> is used to integrate Identity with the DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // use this to apply the Identity configurations

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        

    }
}
