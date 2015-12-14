using System.Linq;
using System.Xml.Linq;
using HtmlAgilityPack;
using CrawlerContracts;
using System.Collections.Generic;
using System;
using System.Windows.Forms;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace IndianNewsCrawler
{
    public class NDTVCrawler
    {
        private const string _feedUrl = "http://feeds.feedburner.com/NdtvNews-TopStories?format=xml";
        private Utils _utils;
        private List<TryTemplateDelegate> _templates = new List<TryTemplateDelegate>();

        private NDTVCrawler(Utils utils)
        {
            _utils = utils;
            _templates.Add(new TryTemplateDelegate(TryTemplateOne));
            _templates.Add(new TryTemplateDelegate(TryTemplateTwo));
            _templates.Add(new TryTemplateDelegate(TryTemplateThree));
            _templates.Add(new TryTemplateDelegate(TryTemplateFour));
        }

        public static NDTVCrawler GetNDTVCrawler()
        {
            var utils = Utils.Instance;
            return new NDTVCrawler(utils);
        }

        public IEnumerable<string> CrawlNDTVPage(string url)
        {
            var webpage = new HtmlWeb();
            HtmlDocument doc = webpage.Load(url);
            List<string> imageArray = null;
            foreach (var template in _templates)
            {
                imageArray = template(doc);
                if (imageArray.Any()) { break; }
            }
            return imageArray.Select(x => _utils.RelativeToAbsoluteUri(url, x));
        }

        #region Image Extraction Templates

        private List<string> TryTemplateFour(HtmlDocument doc)
        {
        // Sample Url: http://profit.ndtv.com/news/tax/article-income-tax-relief-court-appeal-by-authorities-for-rs-10-lakh-and-above-1254429
            return _utils.TemplateByClassAndImgTag(doc, "storypicbig");
        }

        private List<string> TryTemplateThree(HtmlDocument doc)
        {
            // Sample Url: http://sports.ndtv.com/cricket/news/253136-india-pakistan-bilateral-series-a-closed-chapter-shahryar-khan
            return _utils.TemplateByClassAndImgTag(doc, "stry-ft-img");
        }

        private List<string> TryTemplateTwo(HtmlDocument doc)
        {
            // Sample Url: http://movies.ndtv.com/bollywood/why-saira-banos-glad-dilip-kumar-didnt-do-film-with-elizabeth-taylor-1254421
            return _utils.TemplateByClassAndImgTag(doc, "ndmv-common-img-wrapper");
        }

        private List<string> TryTemplateOne(HtmlDocument doc)
        {
            // Sample Url: http://www.ndtv.com/delhi-news/500-shanties-razed-1-child-killed-in-anti-encroachment-drive-in-delhi-1254302
            return _utils.TemplateByClassAndImgTag(doc, "ins_mainimage_big story_pic");
        }
        #endregion Image Extraction Templates

        public IEnumerable<Feed> LoadRssFeeds()
        {
            XDocument xDoc = XDocument.Load(_feedUrl);
            var feeds = xDoc.Descendants("item")
                .Select(
                    t =>
                        new Feed
                        {
                            Title = t.Element("title").Value,
                            Link = t.Element("link").Value,
                            PublishedDate = DateTime.Parse(t.Element("pubDate").Value),
                            Description = HtmlRemoval.StripTagsRegex(t.Element("description").Value)
                        }).ToList();
            return feeds;
        }

        public IEnumerable<ShortStory> GetShortStories()
        {
            var feeds = LoadRssFeeds();
            return ConvertToShortStory(feeds);
        }

        private IEnumerable<ShortStory> ConvertToShortStory(IEnumerable<Feed> feeds)
        {
            var shortStories = feeds.Select(x =>
            {
                var realUrl = _utils.GetRealUrl(x.Link);
                var images = CrawlNDTVPage(realUrl);
                if (!images.Any()) { Console.WriteLine("Could not extract image for \n{0}", x.Link); Console.ReadKey(); return null; }
                return new ShortStory
                {
                    Guid = Guid.NewGuid().ToString(),
                    Title = x.Title,
                    Link = realUrl,
                    Description = x.Description,
                    PublishedDate = x.PublishedDate,
                    ImageUrl = images.First(),
                    Source = Source.NDTV
                };
            }).Where(x => x != null).ToList();
            return shortStories;
        }
    }
}
