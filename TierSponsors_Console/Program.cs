using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using TierSponsors_Console.srTierSponsorsService; 

namespace TierSponsors_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var clientTierSponsorsServiceClient = new TierSponsorsServiceClient();
            string jsonString = clientTierSponsorsServiceClient.GetOrganisations("london", "", "", "Tier 2", "");

            byte[] byteArray = Encoding.Unicode.GetBytes(jsonString);
            MemoryStream stream = new MemoryStream(byteArray);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(OrganisationsCollection));
            OrganisationsCollection organisations = (OrganisationsCollection)serializer.ReadObject(stream);

            Console.WriteLine("Count: " + organisations.TotalOrgs);
            Console.WriteLine();

            var index = 0;
            foreach (var org in organisations.OrgList)
            {
                Console.Write(index + 1 + ". ");
                if (!String.IsNullOrEmpty(org.Name)) Console.WriteLine(org.Name);
                if (!String.IsNullOrEmpty(org.City)) Console.WriteLine(org.City);
                if (!String.IsNullOrEmpty(org.County)) Console.WriteLine(org.County);

                foreach (var tier in org.TierList)
                {
                    if (!String.IsNullOrEmpty(tier.TierType)) Console.WriteLine(tier.TierType);
                    if (!String.IsNullOrEmpty(tier.Rating)) Console.WriteLine(tier.Rating);
                    if (!String.IsNullOrEmpty(tier.SubTier)) Console.WriteLine(tier.SubTier);
                }
                Console.WriteLine();
                index++;
            }

            Console.ReadKey();
        }
    }

    
}
