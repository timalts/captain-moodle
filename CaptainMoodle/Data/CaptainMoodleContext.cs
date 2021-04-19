using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Account;
using Models;

namespace CaptainMoodle.Data
{
    public class CaptainMoodleContext : DbContext
    {
        public CaptainMoodleContext(DbContextOptions<CaptainMoodleContext> options) : base(options)
        {

        }

        public DbSet<Admin> Admin { get; set; }

        public DbSet<Faculty> Faculty { get; set; }

        public DbSet<Student> Student { get; set; }

        public DbSet<Payment> Payment { get; set; }
    }
}
