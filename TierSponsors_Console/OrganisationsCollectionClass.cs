using System.Collections.Generic;

namespace TierSponsors_Console
{
    public class OrganisationsCollection
    {
        public int TotalOrgs { get { return OrgList.Count; } }
        public List<Organisation> OrgList { get; set; }
    }

    public class Organisation
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public List<Tier> TierList { get; set; }
    }

    public class Tier
    {
        public string TierType { get; set; }
        public string Rating { get; set; }
        public string SubTier { get; set; }
    }
}
