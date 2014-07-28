using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TierSponsors_DataModel;

namespace TierSponsors_MakerBetter
{
    class Program
    {
        static void Main(string[] args)
        {
            MakeLocationBetter();
            Console.ReadKey();
        }

        private static void MakeLocationBetter()
        {
            TextReader reader = File.OpenText("commands.txt");

            while (true)
            {
                var command = reader.ReadLine();
                if (String.IsNullOrEmpty(command)) break;
                Console.WriteLine(ReplaceCounty(command));
            }
            reader.Close();

            Console.WriteLine();
            Console.WriteLine("Done");
            Console.WriteLine();
        }

        private static string ReplaceCounty(string command)
        {
            if (command.IndexOf(" >> ", StringComparison.Ordinal) == -1) return "Nothing to change: " + command;
            var splitted = command.Replace(" >> ",">").Split('>');
            if (splitted.Length != 2) return "Sometring goes wrong: " + command;
            
            var wrong = splitted[0];
            var right = splitted[1];

            var db = new DataContext();
            var orgs = db.Organisations.Where(x => x.TempCounty == wrong).ToList();
            foreach (var org in orgs)
            {
                org.TempCounty = right;
                org.DateChanges = DateTime.Now;
            }
            db.SaveChanges();
            return "Replaced county:   " + wrong + " => " + right + " - " + orgs.Count();
        }
    }
}
