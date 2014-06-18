using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TierSponsors_Web.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("database")
        {
        }

        public DbSet<Organisation> Organisations { get; set; }
    }
}