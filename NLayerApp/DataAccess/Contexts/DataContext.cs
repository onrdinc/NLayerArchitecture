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
        public DataContext(DbContextOptions<DataContext> context) : base(context)
        {
            
        }

        public DbSet<Banks> Banks { get; set; }
        public DbSet<MyCards> MyCards { get; set; }

    }
}
