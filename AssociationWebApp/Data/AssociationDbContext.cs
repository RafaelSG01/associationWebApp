using AssociationWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssociationWebApp.Data
{
    public class AssociationDbContext : DbContext
    {
        public AssociationDbContext(DbContextOptions<AssociationDbContext> options) : base(options)
        {

        }
        public DbSet<Associated> Associated { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Association> Association { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Association>().HasKey(bc => new { bc.Id });
        }

    }
}
