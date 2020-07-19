using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ReadingRSS.Models
{
    public class ReadingRSSContext : DbContext
    {
        public ReadingRSSContext() : base("ReadingRSS")
        {
        }

        public DbSet<News> News { get; set; }
        public DbSet<Source> Sources { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}