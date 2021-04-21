using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaptaineMoodle.Areas.Identity.Data;
using CaptaineMoodle.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace CaptaineMoodle.Areas.Identity.Data
{
    public class AuthDbContext : IdentityDbContext<User>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {


        }
        public DbSet<Payment> Payment { get; set; }

        public DbSet<Course> Course { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }

    
}
