using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace TierSponsors_MigrateDB
{
    class Program
    {
        static void Main(string[] args)
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["DB_ConnectionString"].ConnectionString;
            var connectionString = "Data Source=sql07-8-eng.corp.parking.ru;Initial Catalog=yourtown_20;User ID=yourtown_20;Password=[6[b!Lpc-^";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var da = new SqlDataAdapter("select * from yourtown_20.dbo.Organisation", connectionString);
                var ds = new DataSet();
                da.Fill(ds);

            }
        }
    }
}
