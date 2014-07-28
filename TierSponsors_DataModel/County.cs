using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TierSponsors_DataModel
{
    public class County
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LongName { get; set; }
        public Country Country { get; set; }
    }
}
