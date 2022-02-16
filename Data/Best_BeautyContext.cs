using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Best_Beauty.Models;

namespace Best_Beauty.Data
{
    public class Best_BeautyContext : DbContext
    {
        public Best_BeautyContext (DbContextOptions<Best_BeautyContext> options)
            : base(options)
        {
        }

        public DbSet<Best_Beauty.Models.Service> Service { get; set; }

        public DbSet<Best_Beauty.Models.Client> Client { get; set; }

        public DbSet<Best_Beauty.Models.Product> Product { get; set; }

        public DbSet<Best_Beauty.Models.Category> Category { get; set; }
    }
}
