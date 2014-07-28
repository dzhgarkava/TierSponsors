using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TierSponsors_DataModel
{
    public class Organisation
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public List<Visa> Visas { get; set; }
        public City City { get; set; }
        public County County { get; set; }
        public Country Country { get; set; }
        public string Hash { get; set; }
        public string Comment { get; set; }
        public DateTime DateChanges { get; set; }
        public bool Authorized { get; set; }
        public string UkbaOriginal { get; set; }
        public string TempCounty { get; set; }
        public string TempCity { get; set; }
    }
}
