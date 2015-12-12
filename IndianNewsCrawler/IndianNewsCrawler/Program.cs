using System;
using CrawlerContracts;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace IndianNewsCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            //var feeds = NDTVCrawler.LoadRssFeeds("http://timesofindia.indiatimes.com/rssfeedsdefault.cms");
            var feeds = NDTVCrawler.LoadRssFeeds("http://feeds.feedburner.com/NdtvNews-TopStories");
            var shortStoriesByDate = feeds.Select(x => new ShortStory { Guid = Guid.NewGuid().ToString(), 
                                                                        Title = x.Title, 
                                                                        Link = x.Link, 
                                                                        Description = x.Description, 
                                                                        PublishedDate = x.PublishedDate, 
                                                                        ImageUrl = NDTVCrawler.CrawlTOIPage(x.Link),
                                                                        Source = Source.TOI})
                                            .GroupBy(x=>x.PublishedDate.Date);
            shortStoriesByDate.ToList().ForEach(x => File.WriteAllText(String.Format("{0}.txt",x.Key.ToShortDateString()),JsonConvert.SerializeObject(x.ToArray())));
            
        }
    }
}
