using System.Linq;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace IndianNewsCrawler
{
    class TOICrawler
    {
        public static Article CrawlTOIPage(string url)
        {
            var webpage = new HtmlWeb();
            HtmlDocument doc = webpage.Load(url);
            return Parse(doc);
        }

        public static Article Parse(HtmlDocument doc)
        {
            var article = new Article {Source = Source.TOI};
            // Extract title, text, images
            var titleNode = Utils.GetNodeOfAClass(doc, "span", "arttle");
            article.Title = titleNode.InnerText;
            var articleTextNode = doc.GetElementbyId("artext1");
            RemoveExternalAnchors(articleTextNode);
            RemoveStrongNodes(articleTextNode);
            article.ArticleText = doc.GetElementbyId("artext1").InnerText;
            return article;
        }

        public static void RemoveStrongNodes(HtmlNode node)
        {
            var strongNodes = node.Descendants("strong").ToArray();
            foreach (var strongNode in strongNodes)
            {
                strongNode.Remove();
            }            
        }

        public static void RemoveExternalAnchors(HtmlNode node)
        {
            var externalLinkNodes = node.Descendants("a")
                .Where(t => t.Attributes.Contains("href") && !t.Attributes["href"].Value.StartsWith("#")).ToArray();
            foreach (var externalLinkNode in externalLinkNodes)
            {
                externalLinkNode.Remove();
            }
        }

        public static Article[] GetHomepageUrls()
        {
            const string homepageUrl = "http://timesofindia.indiatimes.com/international-home";
            const string domain = "http://timesofindia.indiatimes.com/";
            var webpage = new HtmlWeb();
            HtmlDocument doc = webpage.Load(homepageUrl);
            var topStoriesNode = Utils.GetNodeOfAClass(doc, "div", "top-story");
            var topStories = topStoriesNode.Descendants("a").Where(t => t.Attributes.Contains("href"))
                    .Select(t => domain + t.Attributes["href"].Value).ToArray();
            return topStories.Select(CrawlTOIPage).ToArray();
        }


        public static Feed[] LoadRssFeeds(string feedUrl)
        {
            XDocument xDoc = XDocument.Load(feedUrl);
            var feeds = xDoc.Descendants("item")
                .Select(
                    t =>
                        new Feed
                        {
                            Title = t.Element("title").Value,
                            Link = t.Element("link").Value,
                            PublishedDate = t.Element("pubDate").Value,
                            Description = t.Element("description").Value
                        }).ToArray();
            return feeds;
        }
    }

    public class Feed
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string PublishedDate { get; set; }
        public string Description { get; set; }
    }
}
