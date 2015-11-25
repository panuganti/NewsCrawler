using System.Linq;
using HtmlAgilityPack;

namespace IndianNewsCrawler
{
    class Utils
    {
        public static HtmlNode GetNodeOfAClass(HtmlDocument doc, string descendentType, string classname)
        {
            return doc.DocumentNode.Descendants(descendentType)
                    .First(node => node.Attributes.Contains("class") && node.Attributes["class"].Value == classname);
        }
    }
}
