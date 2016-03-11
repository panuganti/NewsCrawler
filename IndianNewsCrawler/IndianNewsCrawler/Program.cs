using System;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace IndianNewsCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var toiCrawler = TOICrawler.GetTOICrawler();
            var toiShortStories = toiCrawler.GetShortStories();
            toiShortStories.GroupBy(x=>x.PublishedDate.Date).ToList()
                .ForEach(x => File.WriteAllText(string.Format("{0}_{1}_{2}.toi.json",x.Key.Year, x.Key.Month, x.Key.Day),
                                                            JsonConvert.SerializeObject(x.ToArray(), Formatting.Indented)));
         */       
            var ndtvCrawler = NDTVCrawler.GetNDTVCrawler();
            var ndtvShortStories = ndtvCrawler.GetShortStories();
            ndtvShortStories.GroupBy(x => x.PublishedDate.Date).ToList()
                .ForEach(x => File.WriteAllText(string.Format("{0}_{1}_{2}.ndtv.json", x.Key.Year, x.Key.Month, x.Key.Day), 
                                                            JsonConvert.SerializeObject(x.ToArray(), Formatting.Indented)));
            
            Console.WriteLine("Writing feeds completed...");
            Console.ReadLine();
        }
    }
}
