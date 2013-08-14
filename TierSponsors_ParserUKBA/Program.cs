using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using PDFCreatorPilotLib;

namespace TierSponsors_ParserUKBA
{
    class Program
    {
        static void Main(string[] args)
        {
            PDFDocument4Class pdf = new PDFDocument4Class();
            pdf.Open("pbsregisterofsponsors_tier2.pdf", "");
            int pagecount = pdf.GetPageCount();
            string str = pdf.GetPageText(1);

            List<String> block = new List<String>();

            for (int i = 0; i < pagecount; i++)
            {
                //Console.Write(pdf.GetPageText(i));

                string page = pdf.GetPageText(i);
                int pos;
                while ((pos = page.IndexOf("\r\n")) != -1)
                {
                    block.Add(page.Remove(pos));
                    page = page.Remove(0, pos + 2);
                }
            }

            while (block[0].ToString().IndexOf("Organisation Name Town/City County Tier & Rating Sub Tier") == -1)
            {
                block.RemoveAt(0);
            }
            block.RemoveAt(0);

            for (int i = 0; i < block.Count; i++)
            {
                if (block[i].ToString().IndexOf("Organisation Name Town/City County Tier & Rating Sub Tier") != -1)
                {
                    block.RemoveAt(i);
                    i--;
                }
            }

            // /*
            // Определяем различные типы виз и их разновидности
            List<String> Tiers = new List<String>();
            List<String> SubTiers = new List<String>();

            while (block.Count > 0)
            {
                if (block[0].ToString().IndexOf(" rating)") == -1)
                {
                    if (block.Count == 0) break;

                    Organisation Org = new Organisation();
                    Org.Name = block[0].ToString();
                    //Console.WriteLine(block[0].ToString());
                    block.RemoveAt(0);

                    if (block.Count == 0) break;
                    while (block[0].ToString().IndexOf(" rating)") != -1)
                    {
                        //Console.WriteLine(block[0].ToString());
                        String tierandrating = block[0].ToString();
                        Org.TierAndRating += tierandrating.Remove(tierandrating.IndexOf(" rating)") + 8) + ";";
                        Org.SubTier += tierandrating.Remove(0, tierandrating.IndexOf(" rating)") + 9) + ";";

                        // проверяем разные Тиер
                        bool addtier = true;
                        for (int t = 0; t < Tiers.Count; t++)
                        {
                            if (Tiers[t].ToString() == tierandrating.Remove(tierandrating.IndexOf(" rating)") + 8)) addtier = false;
                        }
                        if (addtier) Tiers.Add(tierandrating.Remove(tierandrating.IndexOf(" rating)") + 8));


                        if (tierandrating.Remove(tierandrating.IndexOf(" rating)") + 8).IndexOf("Tier 5") != -1)
                        {
                            bool addsubtier = true;
                            for (int t = 0; t < SubTiers.Count; t++)
                            {
                                if (SubTiers[t].ToString() == tierandrating.Remove(0, tierandrating.IndexOf(" rating)") + 9)) addsubtier = false;
                            }
                            if (addsubtier) SubTiers.Add(tierandrating.Remove(0, tierandrating.IndexOf(" rating)") + 9));
                        }

                        block.RemoveAt(0);
                        if (block.Count == 0) break;
                    }


                }
                Console.WriteLine();
            }
            //*/

            int totalcount = 0;

            // Пищем в базу

            String connectionString = "Data Source=project10-devel;Initial Catalog=test_ilawiuk;Integrated Security=True;User ID=pmo_reporting;Password=Ps71_iE";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlDataAdapter da = new SqlDataAdapter("select * from test_ilawiuk.dbo.tdMain", connectionString);
                DataSet ds = new DataSet();
                da.Fill(ds);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);

                while (block.Count > 0)
                {
                    if (block[0].ToString().IndexOf(" rating)") == -1)
                    {
                        if (block.Count == 0) break;

                        Organisation Org = new Organisation();
                        Org.Name = block[0].ToString();
                        //Console.WriteLine(block[0].ToString());
                        block.RemoveAt(0);

                        if (block.Count == 0) break;
                        while (block[0].ToString().IndexOf(" rating)") != -1)
                        {
                            //Console.WriteLine(block[0].ToString());
                            String tierandrating = block[0].ToString();
                            Org.TierAndRating += tierandrating.Remove(tierandrating.IndexOf(" rating)") + 8) + ";";
                            Org.SubTier += tierandrating.Remove(0, tierandrating.IndexOf(" rating)") + 9) + ";";

                            block.RemoveAt(0);
                            if (block.Count == 0) break;
                        }

                        Console.WriteLine(Org.Name.ToString());

                        DataRow dr = ds.Tables[0].NewRow();
                        dr["NameCityCounty"] = Org.Name;
                        dr["TierAndRating"] = Org.TierAndRating;
                        dr["SubTier"] = Org.SubTier;
                        dr["OriginalUKBA"] = Org.Name;
                        ds.Tables[0].Rows.Add(dr);

                    }
                    //Console.WriteLine();
                }

                builder.GetUpdateCommand();
                da.Update(ds);
                connection.Close();
            }

            Console.WriteLine();
            Console.WriteLine("Total: " + totalcount);
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
