using System.Linq;
using System.Xml.Linq;
using HtmlAgilityPack;
using CrawlerContracts;
using System.Collections.Generic;
using System;

namespace IndianNewsCrawler
{
    class NDTVCrawler
    {
        public static string CrawlTOIPage(string url)
        {
            var webpage = new HtmlWeb();
            HtmlDocument doc = webpage.Load(url);
            var imageNodes = doc.DocumentNode.Descendants("img").Where(x => x.Attributes.Contains("src")).Select(x => x.Attributes["src"].Value.ToString());
            var localImageNodes = imageNodes.Where(x => x.StartsWith("/")).ToArray();
            return localImageNodes.First(); ;
        }


        public static IEnumerable<Feed> LoadRssFeeds(string feedUrl)
        {
            XDocument xDoc = XDocument.Load(feedUrl);
            var feeds = xDoc.Descendants("item")
                .Select(
                    t =>
                        new Feed
                        {
                            Title = t.Element("title").Value,
                            Link = t.Element("link").Value,
                            PublishedDate = DateTime.Parse(t.Element("pubDate").Value),
                            Description = t.Element("description").Value
                        });
            return feeds;
        }
    }
}
