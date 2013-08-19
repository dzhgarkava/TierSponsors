using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace TierSponsors_EditDB.Web
{

    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceEditDB_SL
    {
        // Use it for technoserv database
        //private const string DataBaseName = "TierSponsors_DB";
        //readonly string _connectionString = ConfigurationManager.ConnectionStrings["TS_ConnectionString"].ConnectionString;

        // Use it for parking database
        private const string DataBaseName = "yourtown_20"; 
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["PARKING_ConnectionString"].ConnectionString;
        

        [OperationContract]
        public OrganisationsCollection GetOrganisations(bool Checked)
        {
            var organisations = new OrganisationsCollection
            {
                OrgList = new List<Organisation>()
            };

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var searchQuery = Checked ? "Checked LIKE 'OK'" : "Checked LIKE 'FALSE'";
                var da = new SqlDataAdapter("select * from " + DataBaseName + ".dbo.Organisations where " + searchQuery, _connectionString);
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
                    };
                    organisations.OrgList.Add(org);
                }
            }
            return organisations;
        }

        [OperationContract]
        public Organisation GetOrganisationByID(string ID)
        {
            Organisation org;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var da = new SqlDataAdapter("select * from " + DataBaseName + ".dbo.Organisations", _connectionString);
                var ds = new DataSet();
                da.Fill(ds);

                var searchQuery = "ID = " + ID;

                DataRow[] dr = ds.Tables[0].Select(searchQuery);

                org = dr.Any()
                    ? new Organisation
                    {
                        ID = dr[0]["ID"].ToString(),
                        Name = dr[0]["Name"].ToString(),
                        City = dr[0]["City"].ToString(),
                        County = dr[0]["County"].ToString(),
                        NameCityCounty = dr[0]["NameCityCounty"].ToString()
                    }
                    : null;
            }
            return org;
        }

        [OperationContract]
        public void SetOrganisationByID(string ID, string Name, string City, string County, string NameCityCounty, string Checked)
        {
            Organisation org;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var da = new SqlDataAdapter("select * from " + DataBaseName + ".dbo.Organisations where ID = " + ID, _connectionString);
                var ds = new DataSet();
                da.Fill(ds);

                DataRow[] dr = ds.Tables[0].Select();

                if (dr.Any())
                {
                    if (dr[0]["ID"].ToString().Equals(ID))
                    {
                        dr[0]["Name"] = Name;
                        dr[0]["City"] = City;
                        dr[0]["County"] = County;
                        dr[0]["Checked"] = Checked;
                        dr[0]["NameCityCounty"] = NameCityCounty;
                    }
                }

                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                builder.GetUpdateCommand();
                da.Update(ds);
                connection.Close();
            }
        }

        // Add more operations here and mark them with [OperationContract]
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
            public string NameCityCounty { get; set; }
        }
    }
}
