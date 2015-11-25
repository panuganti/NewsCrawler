using System;

namespace IndianNewsCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            //var articles = TOICrawler.GetHomepageUrls();
            var feeds = TOICrawler.LoadRssFeeds("http://timesofindia.indiatimes.com/rssfeedsdefault.cms");
            Console.WriteLine(feeds.Length);
        }
    }
}
