using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TierSponsors_Service.Helper;

namespace TierSponsors_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TierSponsorsService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TierSponsorsService.svc or TierSponsorsService.svc.cs at the Solution Explorer and start debugging.
    public class TierSponsorsService : ITierSponsorsService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        private OrganisationsCollection SearchOrganisations(string searchQuery)
        {
            var organisations = new OrganisationsCollection
            {
                OrgList = new List<Organisation>()
            };

            var connectionString = ConfigurationManager.ConnectionStrings["DB_ConnectionString"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var da = new SqlDataAdapter("select * from test_ilawiuk.dbo.tdMain", connectionString);
                var ds = new DataSet();
                da.Fill(ds);

                DataRow[] dr = ds.Tables[0].Select(searchQuery);
                for (int i = 0; i < dr.Count(); i++)
                {
                    var org = new Organisation
                    {
                        ID = dr[i]["ID"].ToString(),
                        Name = dr[i]["Name"].ToString(),
                        City = dr[i]["City"].ToString(),
                        County = dr[i]["County"].ToString(),
                        TierList = new List<Tier>()
                    };
                    organisations.OrgList.Add(org);
                }
            }
            return organisations;
        }

        public string GetOrganisations(string name, string city, string county, string tier, string subtier)
        {
            string _name = String.IsNullOrEmpty(name) ? "%" : "%" + name + "%";
            string _city = String.IsNullOrEmpty(city) ? "%" : "%" + city + "%";
            string _county = String.IsNullOrEmpty(county) ? "%" : "%" + county + "%";
            string _tier = String.IsNullOrEmpty(tier) ? "%" : "%" + tier + "%";
            string _subtier = String.IsNullOrEmpty(subtier) ? "%" : subtier;

            // TODO: Add search by count = NULL
            //var searchString = "Name LIKE '" + _name + "' AND City LIKE '" + _city + "' AND County LIKE '" + _county + "' AND TierAndRating LIKE '" + _tier + "' AND SubTier LIKE '" + _subtier + "' AND Checked LIKE 'OK'";
            var searchString = "Name LIKE '" + _name + "' AND City LIKE '" + _city + "' AND TierAndRating LIKE '" + _tier + "' AND SubTier LIKE '" + _subtier + "' AND Checked LIKE 'OK'";
            return SearchOrganisations(searchString).ToJSON();
        }

        public string GetOrganisationsByName(string name)
        {
            var searchString = "Name LIKE '%" + name + "%' AND Checked LIKE 'OK'";
            return SearchOrganisations(searchString).ToJSON();
        }

        public string GetOrganisationsByCity(string city)
        {
            var searchString = "City LIKE '%" + city + "%' AND Checked LIKE 'OK'";
            return SearchOrganisations(searchString).ToJSON();
        }
    }
}
