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
    public delegate List<string> TryTemplateDelegate(HtmlDocument doc);

    public class TOICrawler
    {
        private const string _feedUrl = "http://timesofindia.indiatimes.com/rssfeedsdefault.cms";
        private Utils _utils;
        private List<TryTemplateDelegate> _templates = new List<TryTemplateDelegate>();

        private TOICrawler(Utils utils)
        {
            _utils = utils;
            _templates.Add(new TryTemplateDelegate(TryTemplateOne));
            _templates.Add(new TryTemplateDelegate(TryTemplateTwo));
            _templates.Add(new TryTemplateDelegate(TryTemplateThree));
            _templates.Add(new TryTemplateDelegate(TryTemplateFour));
            _templates.Add(new TryTemplateDelegate(TryTemplateFive));
            _templates.Add(new TryTemplateDelegate(TryTemplateSix));
        }

        public static TOICrawler GetTOICrawler()
        {
            var utils = Utils.Instance;
            return new TOICrawler(utils);
        }

        public IEnumerable<string> CrawlTOIPage(string url)
        {
            try {
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
            catch(Exception e)
            {
                Console.WriteLine("{0} \n Extraction throws exception {1}", url, e.Message);
                Console.ReadLine();
                return new List<string>();
            }
        }

        #region Image Extraction Templates
                
        private List<string> TryTemplateSix(HtmlDocument doc)
        {
            // Sample Url: http://timesofindia.indiatimes.com/city/mumbai/Mumbai-artist-Hema-Upadhyaya-her-lawyer-found-dead-in-drain-bodies-kept-in-cardboard-boxes/articleshow/50160596.cms
            var imageArray = new List<string>();
            try
            {
                var imageDiv = doc.DocumentNode.Descendants("div").First(x => x.Attributes.Contains("id") && x.Attributes["id"].Value == "artext1");
                var imageUrl = imageDiv.Descendants("img").First(x => x.Attributes.Contains("src")).Attributes["src"].Value;
                imageArray.Add(imageUrl);
            }
            catch (Exception)
            {
            }
            return imageArray;
        }

        private List<string> TryTemplateFive(HtmlDocument doc)
        {
            // Sample Url: http://beautypageants.indiatimes.com/world-pageants/archives/Victoria-Daropale-A-queen-is-one-who-doesnt-seduce-a-man-with-her-body/articleshow/50150493.cms
            return _utils.TemplateByClassAndImgTag(doc, "foto_mgshow");
        }

        private List<string> TryTemplateFour(HtmlDocument doc)
        {
            // Sample Url: http://timesofindia.indiatimes.com/india/No-need-to-give-GST-priority-as-many-vital-bills-pending-Congress/articleshow/50156624.cms
            return _utils.TemplateByClassAndImgTag(doc, "content");
        }

        private List<string> TryTemplateThree(HtmlDocument doc)
        {
            var imageArray = new List<string>();
            try
            {
            // Sample Url: http://timesofindia.indiatimes.com/india/India-set-to-develop-own-stealth-combat-drones/articleshow/50165296.cms
                var imageDiv = doc.DocumentNode.Descendants("div").First(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "main-content")
                                                .Descendants("section").First(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "highlight clearfix");
                var imageUrl = imageDiv.Descendants("img").First(x => x.Attributes.Contains("src")).Attributes["src"].Value;
                imageArray.Add(imageUrl);
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
                var imageDiv = doc.DocumentNode.Descendants("div").First(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "main-content")
                                                .Descendants("div").First(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "preloader");
                var imageUrl = imageDiv.Descendants("img").First(x => x.Attributes.Contains("src")).Attributes["src"].Value;
                imageArray.Add(imageUrl);
            }
            catch (Exception)
            {
            }
            return imageArray;
        }

        private List<string> TryTemplateOne(HtmlDocument doc)
        {
            // Sample Url: http://timesofindia.indiatimes.com/home/environment/global-warming/196-countries-approve-historic-Paris-global-warming-accord/articleshow/50155629.cms
            return _utils.TemplateByClassAndImgTag(doc, "flL_pos");
        }
        #endregion Image Extraction Templates

        public List<Feed> LoadRssFeeds()
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
                            Description = t.Element("description").Value
                        });
            return feeds.ToList();
        }

        public List<ShortStory> GetShortStories()
        {
            var feeds = LoadRssFeeds();
            return ConvertToShortStory(feeds);
        }

        internal List<ShortStory> ConvertToShortStory(IEnumerable<Feed> feeds)
        {
            var shortStories = feeds.Select(x =>
            {
                var realUrl = _utils.GetRealUrl(x.Link);                
                var images = CrawlTOIPage(realUrl);
                if (!images.Any()) { Console.WriteLine("Could not extract image for \n{0}", x.Link); Console.ReadLine();  return null; }
                return new ShortStory
                {
                    Guid = Guid.NewGuid().ToString(),
                    Title = x.Title,
                    Link = realUrl,
                    Description = x.Description,
                    PublishedDate = x.PublishedDate,
                    ImageUrl = images.First(),
                    Source = Source.TOI
                };
            }).Where(x=>x!=null).ToList();
            return shortStories;
        }

    }
}
