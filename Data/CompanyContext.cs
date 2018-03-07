using Company.Models.BusinessLogic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Data
{
    public class CompanyContext : DbContext
    {
        public DbSet<Material> Materials { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<ObjectOfExpenditure> ObjectOfExpenditures { get; set; }

        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Provider>()
                .HasOne(e => e.Material)
                .WithMany(e => e.Providers)
                .Metadata.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
