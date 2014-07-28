using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TierSponsors_DataModel
{
    public class Visa
    {
        public int Id { get; set; }
        public VisaType Type { get; set; }
        public SubType SubType { get; set; }
        public Rating Rating { get; set; }
    }
}
