using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Full_CRUD_Zoo;

namespace FullCRUDZoo.Data
{
    public class ZooContext : DbContext
    {
        public ZooContext (DbContextOptions<ZooContext> options)
            : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().ToTable("Animal");
        }
        
    }
}
