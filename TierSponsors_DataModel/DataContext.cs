using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace TierSponsors_DataModel
{
    public class DataContext: DbContext
    {
        public DataContext()
            : base("Data Source=sql10-12-eng.corp.parking.ru;Initial Catalog=yourtown_43;User ID=yourtown_43;Password=*CI,MhqUhj")
        {
            
        }

        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Visa> Visas { get; set; }
        public DbSet<VisaType> Types { get; set; }
        public DbSet<SubType> SubTypes { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
