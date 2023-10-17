using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.\\SQLEXPRESS;database=Test;trusted_connection=true; trustServerCertificate=true;");
        }

        public DbSet<Banks> Banks { get; set; }
        public DbSet<MyCards> MyCards { get; set; }

    }
}
