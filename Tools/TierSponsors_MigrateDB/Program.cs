using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace TierSponsors_MigrateDB
{
    class Program
    {
        static void Main()
        {
            var connectionStringParking = ConfigurationManager.ConnectionStrings["PARKING_ConnectionString"].ConnectionString;
            using (var connectionParking = new SqlConnection(connectionStringParking))
            {
                connectionParking.Open();

                var daParking = new SqlDataAdapter("select * from yourtown_20.dbo.Organisations", connectionStringParking);
                var dsParking = new DataSet();
                
                daParking.Fill(dsParking);
                
                var connectionStringLocal = ConfigurationManager.ConnectionStrings["LOCAL_ConnectionString"].ConnectionString;
                using (var connectionLocal = new SqlConnection(connectionStringLocal))
                {
                    connectionLocal.Open();

                    var daLocal = new SqlDataAdapter("select * from test_ilawuk.dbo.tdMain", connectionStringLocal);
                    var dsLocal = new DataSet();
                    daLocal.Fill(dsLocal);

                    for (int i = 0; i < dsLocal.Tables[0].Rows.Count; i++)
                    {
                        DataRow drLocal = dsLocal.Tables[0].Rows[i];
                        DataRow drParking = dsParking.Tables[0].NewRow();

                        string[] tierandrating = drLocal["TierAndRating"].ToString().Split(';');
                        string[] subtier = drLocal["SubTier"].ToString().Split(';');
                        string tierratingsubtier = "";

                        if (tierandrating.Length == subtier.Length)
                        {
                            for (int t = 0; t < tierandrating.Length; t++)
                            {
                                if (!String.IsNullOrEmpty(tierandrating[t]) && !String.IsNullOrEmpty(subtier[t]))
                                    tierratingsubtier += tierandrating[t] + ";" + subtier[t] + "\n";
                            }
                        }

                        drParking["ID"] = drLocal["ID"];
                        drParking["Name"] = drLocal["Name"];
                        drParking["City"] = drLocal["City"];
                        drParking["County"] = drLocal["County"];
                        drParking["TierRatingSubTier"] = tierratingsubtier;
                        drParking["NameCityCounty"] = drLocal["NameCityCounty"];
                        drParking["OriginalUKBA"] = drLocal["OriginalUKBA"];
                        drParking["Checked"] = drLocal["Checked"];

                        dsParking.Tables[0].Rows.Add(drParking);
                    }

                    connectionLocal.Close();
                }

                var builder = new SqlCommandBuilder(daParking);
                builder.GetUpdateCommand();
                daParking.Update(dsParking);
                connectionParking.Close();

            }
        }
    }
}
