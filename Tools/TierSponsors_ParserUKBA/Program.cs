using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace TierSponsors_ParserUKBA
{
    class Program
    {
        private const string PathToFile = "pbsregisterofsponsors_tier2.pdf";
        static void Main()
        {
            // Перепивать на работу с текстовым файлом
            //var pdfDocument4 = new PDFDocument4Class();
            //pdfDocument4.Open(PathToFile, "");
            //int pagecount = pdfDocument4.GetPageCount();

            var block = new List<String>();

            //for (int i = 0; i < pagecount; i++)
            //{
            //    string page = pdfDocument4.GetPageText(i);
            //    int pos;
            //    while ((pos = page.IndexOf("\r\n", StringComparison.Ordinal)) != -1)
            //    {
            //        block.Add(page.Remove(pos));
            //        page = page.Remove(0, pos + 2);
            //    }
            //}

            while (block[0].IndexOf("Organisation Name Town/City County Tier & Rating Sub Tier", StringComparison.Ordinal) == -1)
            {
                block.RemoveAt(0);
            }
            block.RemoveAt(0);

            for (int i = 0; i < block.Count; i++)
            {
                if (block[i].IndexOf("Organisation Name Town/City County Tier & Rating Sub Tier", StringComparison.Ordinal) != -1)
                {
                    block.RemoveAt(i);
                    i--;
                }
            }

            // /*
            // Определяем различные типы виз и их разновидности
            var tiers = new List<String>();
            var subTiers = new List<String>();

            while (block.Count > 0)
            {
                if (block[0].IndexOf(" rating)", StringComparison.Ordinal) == -1)
                {
                    if (block.Count == 0) break;

                    var org = new Organisation {Name = block[0]};
                    //Console.WriteLine(block[0].ToString());
                    block.RemoveAt(0);

                    if (block.Count == 0) break;
                    while (block[0].IndexOf(" rating)", StringComparison.Ordinal) != -1)
                    {
                        //Console.WriteLine(block[0].ToString());
                        var tierandrating = block[0];
                        org.TierAndRating += tierandrating.Remove(tierandrating.IndexOf(" rating)", StringComparison.Ordinal) + 8) + ";";
                        org.SubTier += tierandrating.Remove(0, tierandrating.IndexOf(" rating)", StringComparison.Ordinal) + 9) + ";";

                        // проверяем разные Тиер
                        var addtier = true;
                        foreach (string t1 in tiers)
                        {
                            if (t1 == tierandrating.Remove(tierandrating.IndexOf(" rating)", StringComparison.Ordinal) + 8)) addtier = false;
                        }
                        if (addtier) tiers.Add(tierandrating.Remove(tierandrating.IndexOf(" rating)", StringComparison.Ordinal) + 8));

                        if (tierandrating.Remove(tierandrating.IndexOf(" rating)", StringComparison.Ordinal) + 8).IndexOf("Tier 5", StringComparison.Ordinal) != -1)
                        {
                            var addsubtier = true;
                            foreach (string s1 in subTiers)
                            {
                                if (s1 == tierandrating.Remove(0, tierandrating.IndexOf(" rating)", StringComparison.Ordinal) + 9)) addsubtier = false;
                            }
                            if (addsubtier) subTiers.Add(tierandrating.Remove(0, tierandrating.IndexOf(" rating)", StringComparison.Ordinal) + 9));
                        }

                        block.RemoveAt(0);
                        if (block.Count == 0) break;
                    }


                }
                Console.WriteLine();
            }
            //*/

            // Пищем в базу

            const string connectionString = "Data Source=project10-devel;Initial Catalog=test_ilawiuk;Integrated Security=True;User ID=pmo_reporting;Password=Ps71_iE";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var da = new SqlDataAdapter("select * from test_ilawiuk.dbo.tdMain", connectionString);
                var ds = new DataSet();
                da.Fill(ds);
                
                while (block.Count > 0)
                {
                    if (block[0].IndexOf(" rating)", StringComparison.Ordinal) == -1)
                    {
                        if (block.Count == 0) break;

                        var org = new Organisation {Name = block[0]};
                        //Console.WriteLine(block[0].ToString());
                        block.RemoveAt(0);

                        if (block.Count == 0) break;
                        while (block[0].IndexOf(" rating)", StringComparison.Ordinal) != -1)
                        {
                            //Console.WriteLine(block[0].ToString());
                            var tierandrating = block[0];
                            org.TierAndRating += tierandrating.Remove(tierandrating.IndexOf(" rating)", StringComparison.Ordinal) + 8) + ";";
                            org.SubTier += tierandrating.Remove(0, tierandrating.IndexOf(" rating)", StringComparison.Ordinal) + 9) + ";";

                            block.RemoveAt(0);
                            if (block.Count == 0) break;
                        }

                        Console.WriteLine(org.Name);

                        DataRow dr = ds.Tables[0].NewRow();
                        dr["NameCityCounty"] = org.Name;
                        dr["TierAndRating"] = org.TierAndRating;
                        dr["SubTier"] = org.SubTier;
                        dr["OriginalUKBA"] = org.Name;
                        ds.Tables[0].Rows.Add(dr);

                    }
                    //Console.WriteLine();
                }

                var builder = new SqlCommandBuilder(da);
                builder.GetUpdateCommand();
                da.Update(ds);
                connection.Close();
            }

            Console.WriteLine();
            Console.ReadKey();
        }

        private class Organisation
        {
            public String Name { get; set; }
            public String TierAndRating { get; set; }
            public String SubTier { get; set; }
        }
    }
}
