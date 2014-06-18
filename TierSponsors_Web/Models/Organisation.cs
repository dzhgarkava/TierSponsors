using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TierSponsors_Web.Models
{
    public class Organisation
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
    }
}