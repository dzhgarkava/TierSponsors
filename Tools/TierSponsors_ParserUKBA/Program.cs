using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq.Expressions;
using System.Threading;


namespace TierSponsors_ParserUKBA
{
    class Program
    {
        private const string PathToFile = "sponsorslist.txt";
        static void Main()
        {
            var block = new List<String>();

            TextReader reader = File.OpenText(PathToFile);
            string line = "";
            var count = 0;
            var isStarted = false;
            
            while ((line = reader.ReadLine()) != null)
            {
                if (isStarted)
                {
                    if (line.IndexOf("Organisation Name Town/City County Tier & Rating Sub Tier ", StringComparison.Ordinal) == -1)
                        if (!String.IsNullOrEmpty(line))
                            if (!(line.Contains("Page ") && line.Contains(" of ")))
                                if (line != "\f")
                                {
                                    if (line[line.Length - 1] == ' ')
                                        line = line.Remove(line.Length - 1);
                                    block.Add(line);
                                }
                }
                else
                {
                    if (line.IndexOf("Organisation Name Town/City County Tier & Rating Sub Tier ", StringComparison.Ordinal) == 0)
                    {
                        isStarted = true;
                    }
                }
                count++;
            }

            for (int i = 0; i < 4; i++)
            block.RemoveAt(block.Count - 1);

            // Определяем различные типы виз и их разновидности
            //var tiers = new List<String>();
            //var subTiers = new List<String>();

            //while (block.Count > 0)
            //{
            //    if (block[0].IndexOf(" rating)", StringComparison.Ordinal) == -1)
            //    {
            //        if (block.Count == 0) break;

            //        var org = new Organisation {Name = block[0]};
            //        //Console.WriteLine(block[0].ToString());
            //        block.RemoveAt(0);

            //        if (block.Count == 0) break;
            //        while (block[0].IndexOf(" rating)", StringComparison.Ordinal) != -1)
            //        {
            //            //Console.WriteLine(block[0].ToString());
            //            var tierandrating = block[0];
            //            org.TierAndRating += tierandrating.Remove(tierandrating.IndexOf(" rating)", StringComparison.Ordinal) + 8) + ";";
            //            org.SubTier += tierandrating.Remove(0, tierandrating.IndexOf(" rating)", StringComparison.Ordinal) + 9) + ";";

            //            // проверяем разные Тиер
            //            var addtier = true;
            //            foreach (string t1 in tiers)
            //            {
            //                if (t1 == tierandrating.Remove(tierandrating.IndexOf(" rating)", StringComparison.Ordinal) + 8)) addtier = false;
            //            }
            //            if (addtier) tiers.Add(tierandrating.Remove(tierandrating.IndexOf(" rating)", StringComparison.Ordinal) + 8));

            //            if (tierandrating.Remove(tierandrating.IndexOf(" rating)", StringComparison.Ordinal) + 8).IndexOf("Tier 5", StringComparison.Ordinal) != -1)
            //            {
            //                var addsubtier = true;
            //                foreach (string s1 in subTiers)
            //                {
            //                    if (s1 == tierandrating.Remove(0, tierandrating.IndexOf(" rating)", StringComparison.Ordinal) + 9)) addsubtier = false;
            //                }
            //                if (addsubtier) subTiers.Add(tierandrating.Remove(0, tierandrating.IndexOf(" rating)", StringComparison.Ordinal) + 9));
            //            }

            //            block.RemoveAt(0);
            //            if (block.Count == 0) break;
            //        }
            //    }
            //    Console.WriteLine();
            //}

            var orgs = new List<Organisation>();

            for (int i = 0; i < block.Count; i++)
            {
                var org = new Organisation {Name = block[i]};

                while (true)
                {
                    if (i + 1 == block.Count) break;

                    var visainfo = block[i + 1];
                    if (visainfo.Contains(" (Premium))"))
                    {
                        var visa = new Visa
                        {
                            TypeAndRating = visainfo.Remove(visainfo.IndexOf(" (Premium))", StringComparison.Ordinal) + 11),
                            SubType = visainfo.Remove(0, visainfo.IndexOf(" (Premium))", StringComparison.Ordinal) + 12)
                        };
                        org.Visas.Add(visa);
                        i++;
                    }
                    else if (visainfo.Contains(" rating)"))
                    {
                        var visa = new Visa
                        {
                            TypeAndRating = visainfo.Remove(visainfo.IndexOf(" rating)", StringComparison.Ordinal) + 8),
                            SubType = visainfo.Remove(0, visainfo.IndexOf(" rating)", StringComparison.Ordinal) + 9)
                        };
                        org.Visas.Add(visa);
                        i++;
                    }
                    else break;
                }

                orgs.Add(org);
            }

            // Пищем в базу
            //ModeDataToDb(orgs);
            
            Console.WriteLine();
            Console.ReadKey();
        }

        public class Organisation
        {
            public String Name { get; set; }
            public String TierAndRating { get; set; }
            public String SubTier { get; set; }
            public List<Visa> Visas { get; set; }

            public Organisation()
            {
                Visas = new List<Visa>();
            }
        }

        public class Visa
        {
            public string TypeAndRating { get; set; }
            public string SubType { get; set; }
        }

        private static void ModeDataToDb(IEnumerable<Organisation> orgs)
        {
            const string connectionString = "Data Source=project10-devel;Initial Catalog=test_ilawiuk;Integrated Security=True;User ID=pmo_reporting;Password=Ps71_iE";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var da = new SqlDataAdapter("select * from test_ilawiuk.dbo.tdMain", connectionString);
                var ds = new DataSet();
                da.Fill(ds);

                foreach (var org in orgs)
                {
                    DataRow dr = ds.Tables[0].NewRow();
                    dr["NameCityCounty"] = org.Name;
                    dr["TierAndRating"] = org.TierAndRating;
                    dr["SubTier"] = org.SubTier;
                    dr["OriginalUKBA"] = org.Name;
                    ds.Tables[0].Rows.Add(dr);
                }

                var builder = new SqlCommandBuilder(da);
                builder.GetUpdateCommand();
                da.Update(ds);
                connection.Close();
            }
        }
    }
}
