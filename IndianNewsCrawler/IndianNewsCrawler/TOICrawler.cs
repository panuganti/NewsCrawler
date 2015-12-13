using System.Linq;
using System.Xml.Linq;
using HtmlAgilityPack;
using CrawlerContracts;
using System.Collections.Generic;
using System;

namespace IndianNewsCrawler
{
    class TOICrawler
    {
        private const string _domainUrl = "http://timesofindia.indiatimes.com";

        public List<string> CrawlTOIPage(string url)
        {
            var webpage = new HtmlWeb();
            HtmlDocument doc = webpage.Load(url);
            List<string> imageArray;
            // TODO: Use delegates and loopo through them
            imageArray = TryTemplateOne(doc);
            if (!imageArray.Any())
            {
                imageArray = TryTemplateTwo(doc);
            }
            if (!imageArray.Any())
            {
                imageArray = TryTemplateThree(doc);
            }
            return imageArray;
        }

        private string AppendDomainName(string urlPath)
        {
            return _domainUrl + urlPath;
        }

        private List<string> TryPhotogalleryTemplateOne(HtmlDocument doc)
        {
            throw new NotImplementedException();
        }

        private List<string> TryTemplateThree(HtmlDocument doc)
        {
            var imageArray = new List<string>();
            try
            {
                // Sample Url: http://timesofindia.indiatimes.com/india/No-need-to-give-GST-priority-as-many-vital-bills-pending-Congress/articleshow/50156624.cms
                var imageDiv = doc.DocumentNode.Descendants("div").First(x => x.Attributes["class"].Value == "main-content")
                                                .Descendants("section").First(x => x.Attributes["class"].Value == "highlight clearfix");
                var imageUrl = imageDiv.Descendants("img").First(x => x.Attributes.Contains("src")).Attributes["src"].Value;
                imageArray.Add(AppendDomainName(imageUrl));
            }
            catch (Exception)
            {
            }
            return imageArray;
        }

        private List<string> TryTemplateTwo(HtmlDocument doc)
        {
            var imageArray = new List<string>();
            try
            {
                // Sample Url: http://timesofindia.indiatimes.com/india/India-Japan-summit-Pacts-inked-on-bullet-train-defence-nuclear-energy/articleshow/50149738.cms
                var imageDiv = doc.DocumentNode.Descendants("div").First(x => x.Attributes["class"].Value == "main-content")
                                                .Descendants("div").First(x => x.Attributes["class"].Value == "preloader");
                var imageUrl = imageDiv.Descendants("img").First(x => x.Attributes.Contains("src")).Attributes["src"].Value;
                imageArray.Add(AppendDomainName(imageUrl));
            }
            catch (Exception)
            {
            }
            return imageArray;
        }

        private List<string> TryTemplateOne(HtmlDocument doc)
        {
            var imageArray = new List<string>();
            try
            {
                // Sample Url: http://timesofindia.indiatimes.com/home/environment/global-warming/196-countries-approve-historic-Paris-global-warming-accord/articleshow/50155629.cms
                var imageDiv = doc.DocumentNode.Descendants("div").First(x => x.Attributes["class"].Value == "flL_pos");
                var imageUrl = imageDiv.Descendants("img").First(x => x.Attributes.Contains("src")).Attributes["src"].Value;
                imageArray.Add(imageUrl);
            }
            catch (Exception e)
            {
            }
                return imageArray;
        }


        public IEnumerable<Feed> LoadRssFeeds(string feedUrl)
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
