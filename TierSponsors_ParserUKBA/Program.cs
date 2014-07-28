using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using HtmlAgilityPack;
using TierSponsors_DataModel;


namespace TierSponsors_ParserUKBA
{
    class Program
    {
        static void Main()
        {
            var orgsFromHtml = ParseHtml("html_sponsorslist.html");
            var orgsFromText = ParseText("sponsorslist.txt");

            foreach (var org in orgsFromText)
            {
                var found = orgsFromHtml.Where(x => x.Hash.Equals(org.Hash)).ToList();
                if (found.Any())
                {
                    org.Name = found.First().Name;
                    org.City = found.First().City;
                    org.County = found.First().County;
                    org.Authorized = true;
                }
                if (found.Count > 1)
                {
                    var err = true;
                }
            }

            var unathorized = orgsFromText.Count(x => !x.Authorized);
            Console.WriteLine(unathorized);

            var cities = new List<String>();
            var counties = new List<String>();
            var typesandratings = new List<String>();
            var subtypes = new List<String>();

            foreach (var org in orgsFromText)
            {
                if (cities.All(x => x != org.City)) cities.Add(org.City);
                if (counties.All(x => x != org.County)) counties.Add(org.County);
                foreach (var v in org.Visas)
                {
                    if (typesandratings.All(x=>x != v.TypeAndRating)) typesandratings.Add(v.TypeAndRating);
                    if (subtypes.All(x=>x != v.SubType)) subtypes.Add(v.SubType);
                }
            }

            //TextWriter wrRatingAndTypes = File.CreateText("typeasandratings.txt");
            //wrRatingAndTypes.WriteLine("TypeAndRatings: " + typesandratings.Count);
            //foreach (var typesandrating in typesandratings)
            //    wrRatingAndTypes.WriteLine(typesandrating);
            //wrRatingAndTypes.Close();

            //TextWriter wrSubTypes = File.CreateText("subtypes.txt");
            //wrSubTypes.WriteLine("SubTypes: " + subtypes.Count);
            //foreach (var subtype in subtypes)
            //    wrSubTypes.WriteLine(subtype);
            //wrSubTypes.Close();

            //TextWriter wrCities = File.CreateText("cities.txt");
            //wrCities.WriteLine("Cities: " + cities.Count);
            //foreach (var city in cities.OrderBy(s => s))
            //    wrCities.WriteLine(city);
            //wrCities.Close();

            //TextWriter wrCounties = File.CreateText("counties.txt");
            //wrCounties.WriteLine("Counties: " + counties.Count);
            //foreach (var county in counties.OrderBy(s => s))
            //    wrCounties.WriteLine(county);
            //wrCounties.Close();

            // Пищем в базу
            // MoveDataToDb(orgsFromText);
            
            Console.WriteLine();
            Console.ReadKey();
        }

        private static void MoveDataToDb(IEnumerable<OrganisationInfo> orgs)
        {
            var db = new DataContext();
            var count = 0;

            foreach (var org in orgs)
            {
                count++;
                var sponsor = new Organisation
                {
                    Name = org.Name,
                    Hash = org.Hash,
                    DateChanges = DateTime.Now,
                    UkbaOriginal = org.UkbaOriginal,
                    TempCity = org.City,
                    TempCounty = org.County,
                    Visas = new List<Visa>()
                };
                foreach (var visaInfo in org.Visas)
                {
                    var visa = new Visa();
                    var tierandrating = visaInfo.TypeAndRating;
                    if (tierandrating.StartsWith("Tier 2 ") || tierandrating.StartsWith("Tier 5 "))
                    {
                        if (tierandrating.StartsWith("Tier 2 "))
                            visa.Type = db.Types.First(x => x.Name == "Tier 2");
                        
                        if (tierandrating.StartsWith("Tier 5 "))
                            visa.Type = db.Types.First(x => x.Name == "Tier 5");
                        
                        var rating = tierandrating.Remove(0, 7);
                        switch (rating)
                        {
                            case "(A rating)":
                            {
                                visa.Rating = db.Ratings.First(x => x.Name == "A rating" && x.Additional == "");
                                break;
                            }
                            case "(A (Premium))":
                            {
                                visa.Rating = db.Ratings.First(x => x.Name == "A rating" && x.Additional == "Premium");
                                break;
                            }
                            case "(A (SME+))":
                            {
                                visa.Rating = db.Ratings.First(x => x.Name == "A rating" && x.Additional == "SME+");
                                break;
                            }
                            case "(Highly Trusted Sponsor)":
                            {
                                visa.Rating =
                                    db.Ratings.First(
                                        x => x.Name == "A rating" && x.Additional == "Highly Trusted Sponsor");
                                break;
                            }
                            case "(B rating)":
                            {
                                visa.Rating = db.Ratings.First(x => x.Name == "B rating" && x.Additional == "");
                                break;
                            }
                            case "(B (Premium))":
                            {
                                visa.Rating = db.Ratings.First(x => x.Name == "B rating" && x.Additional == "Premium");
                                break;
                            }
                            case "(B (SME+))":
                            {
                                visa.Rating = db.Ratings.First(x => x.Name == "B rating" && x.Additional == "SME+");
                                break;
                            }
                        }
                        
                        visa.SubType = db.SubTypes.First(x => x.Name == visaInfo.SubType);

                        sponsor.Visas.Add(visa);
                    }
                }
                db.Organisations.Add(sponsor);
                Console.WriteLine(sponsor.Name);
                if (count == 500)
                {
                    count = 0;
                    db.SaveChanges();
                    db = new DataContext();
                }
            }
            db.SaveChanges();
        }

        private static string CalculateMd5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        private static List<OrganisationInfo> ParseHtml(string path)
        {
            TextReader reader = File.OpenText(path);
            var html = reader.ReadToEnd();
            html = html.Remove(0, html.IndexOf("<body>", StringComparison.Ordinal) + 6);
            html = html.Replace("</body>", "");
            html = html.Replace("</html>", "");
            html = "<div>" + html.Remove(0, html.IndexOf("<div style=\"position:absolute;left:32.50px;top:187.05px\" class=\"cls_007\">", StringComparison.Ordinal));

            var blocks = new List<String>();

            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var page_divs = doc.DocumentNode.SelectNodes("div");
            foreach (var pdiv in page_divs)
            {
                var divs = pdiv.SelectNodes("div");
                foreach (var div in divs)
                {
                    if (div.InnerHtml.Contains("<img ")) continue;

                    var firstChildText = div.FirstChild.InnerText;
                    if (firstChildText.Equals("Organisation Name")) continue;
                    if (firstChildText.Equals("Town/City")) continue;
                    if (firstChildText.Equals("County")) continue;
                    if (firstChildText.Equals("Tier & Rating")) continue;
                    if (firstChildText.Equals("Sub Tier")) continue;
                    if (firstChildText.Contains("Page ") && div.FirstChild.InnerText.Contains(" of ")) continue;

                    var type = "";
                    var style = div.Attributes["style"].Value;
                    
                    if (style.Contains("left:24.00px;")) type = "Name: ";
                    else if (style.Contains("left:351.00px;") || style.Contains("left:353.49px;")) type = "City: ";
                    else if (style.Contains("left:473.25px;") || style.Contains("left:475.74px;")) type = "County: ";
                    else if (style.Contains("left:576.00px;")) type = "TierAndRating: ";
                    else if (style.Contains("left:678.00px;")) type = "SubTier: ";
                    else if (style.Contains("left:108.13px;")) ;
                    else
                    {
                        var err = true;  
                    }
                    blocks.Add(type + div.FirstChild.InnerText.Replace("  ", " "));

                    //if (type == "TierAndRating: ")
                    //{
                    //    if (tiers.All(x => x != div.FirstChild.InnerText)) tiers.Add(div.FirstChild.InnerText);
                    //}
                }
            }

            for (int i = 0; i < 7; i++)
                blocks.RemoveAt(blocks.Count-1);

            var orgs = new List<OrganisationInfo>();
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].StartsWith("Name: "))
                {
                    var org = new OrganisationInfo {Name = blocks[i].Replace("Name: ", "")};
                    if (i + 1 != blocks.Count)
                    {
                        if (blocks[i + 1].StartsWith("City: "))
                        {
                            org.City = blocks[i + 1].Replace("City: ", "");
                            i++;
                            
                            if (blocks[i + 1].StartsWith("County: "))
                            {
                                org.County = blocks[i + 1].Replace("County: ", "");
                                i++;
                            }
                        }
                        else if (blocks[i + 1].StartsWith("County: "))
                        {
                            org.County = blocks[i + 1].Replace("County: ", "");
                            i++;
                        }
                    }

                    while (true)
                    {
                        if (i + 1 != blocks.Count)
                        {
                            if (blocks[i + 1].StartsWith("TierAndRating: ") && blocks[i + 2].StartsWith("SubTier: "))
                            {
                                var visa = new VisaInfo
                                {
                                    TypeAndRating = blocks[i + 1].Replace("TierAndRating: ", ""),
                                    SubType = blocks[i + 2].Replace("SubTier: ", "")
                                };
                                org.Visas.Add(visa);
                                i++;
                                i++;
                            }
                            else break;
                        }
                        else break;
                    }

                    var orgstring = org.Name;
                    if (!String.IsNullOrEmpty(org.City)) orgstring += " " + org.City;
                    if (!String.IsNullOrEmpty(org.County)) orgstring += " " + org.County;
                    foreach (var visa in org.Visas)
                    {
                        orgstring += " " + visa.TypeAndRating + " " + visa.SubType;
                    }
                    org.Hash = CalculateMd5Hash(orgstring);

                    orgs.Add(org);
                }
            }

            return orgs;
        }

        private static List<OrganisationInfo> ParseText(string path)
        {
            var block = new List<String>();

            TextReader reader = File.OpenText(path);
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

            //        var org = new OrganisationInfo {Name = block[0]};
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

            var orgs = new List<OrganisationInfo>();

            for (int i = 0; i < block.Count; i++)
            {
                var org = new OrganisationInfo { UkbaOriginal = block[i] };

                while (true)
                {
                    if (i + 1 == block.Count) break;

                    var visainfo = block[i + 1];
                    if (visainfo.Contains(" (Premium))"))
                    {
                        var visa = new VisaInfo
                        {
                            TypeAndRating = visainfo.Remove(visainfo.IndexOf(" (Premium))", StringComparison.Ordinal) + 11),
                            SubType = visainfo.Remove(0, visainfo.IndexOf(" (Premium))", StringComparison.Ordinal) + 12)
                        };
                        org.Visas.Add(visa);
                        i++;
                    }
                    else if (visainfo.Contains(" rating)"))
                    {
                        var visa = new VisaInfo
                        {
                            TypeAndRating = visainfo.Remove(visainfo.IndexOf(" rating)", StringComparison.Ordinal) + 8),
                            SubType = visainfo.Remove(0, visainfo.IndexOf(" rating)", StringComparison.Ordinal) + 9)
                        };
                        org.Visas.Add(visa);
                        i++;
                    }
                    else if (visainfo.Contains(" (SME+))"))
                    {
                        var visa = new VisaInfo
                        {
                            TypeAndRating = visainfo.Remove(visainfo.IndexOf(" (SME+))", StringComparison.Ordinal) + 8),
                            SubType = visainfo.Remove(0, visainfo.IndexOf(" (SME+))", StringComparison.Ordinal) + 9)
                        };
                        org.Visas.Add(visa);
                        i++;
                    }
                    else if (visainfo.Contains("Tier 5 (Highly Trusted SpoVnoslourn)tary Workers"))
                    {
                        var visa = new VisaInfo
                        {
                            TypeAndRating = "Tier 5 (Highly Trusted Sponsor)",
                            SubType = "Voluntary Workers"
                        };
                        org.Visas.Add(visa);
                        i++;
                    }
                    else break;
                }

                var orgstring = org.UkbaOriginal;
                foreach (var visa in org.Visas)
                {
                    orgstring += " " + visa.TypeAndRating + " " + visa.SubType;
                }
                org.Hash = CalculateMd5Hash(orgstring);

                orgs.Add(org);
            }
            
            return orgs;
        }

        private static void InitDataBase()
        {
            var db = new DataContext();

            db.Countries.Add(new Country { Name = "England" });
            db.Countries.Add(new Country { Name = "Scotland" });
            db.Countries.Add(new Country { Name = "Wales" });
            db.Countries.Add(new Country { Name = "Northern Ireland" });

            db.Ratings.Add(new Rating { Name = "A rating", Additional = "" });
            db.Ratings.Add(new Rating { Name = "A rating", Additional = "Premium" });
            db.Ratings.Add(new Rating { Name = "A rating", Additional = "SME+" });
            db.Ratings.Add(new Rating { Name = "A rating", Additional = "Highly Trusted Sponsor" });
            db.Ratings.Add(new Rating { Name = "B rating", Additional = "" });
            db.Ratings.Add(new Rating { Name = "B rating", Additional = "Premium" });
            db.Ratings.Add(new Rating { Name = "B rating", Additional = "SME+" });

            db.Types.Add(new VisaType { Name = "Tier 2" });
            db.Types.Add(new VisaType { Name = "Tier 5" });

            db.SubTypes.Add(new SubType { Name = "Tier 2 General" });
            db.SubTypes.Add(new SubType { Name = "Intra Company Transfers (ICT)" });
            db.SubTypes.Add(new SubType { Name = "Creative & Sporting" });
            db.SubTypes.Add(new SubType { Name = "Religious Workers" });
            db.SubTypes.Add(new SubType { Name = "Sport" });
            db.SubTypes.Add(new SubType { Name = "Voluntary Workers" });
            db.SubTypes.Add(new SubType { Name = "Exchange" });
            db.SubTypes.Add(new SubType { Name = "International Agreements" });

            db.SaveChanges();
        }
        public class OrganisationInfo
        {
            public String Name { get; set; }
            public List<VisaInfo> Visas { get; set; }
            public string City { get; set; }
            public string County { get; set; }
            public string Country { get; set; }
            public string Hash { get; set; }
            public string UkbaOriginal { get; set; }
            public bool Authorized { get; set; }

            public OrganisationInfo()
            {
                Visas = new List<VisaInfo>();
            }
        }

        public class VisaInfo
        {
            public string TypeAndRating { get; set; }
            public string SubType { get; set; }
        }
    }
}
