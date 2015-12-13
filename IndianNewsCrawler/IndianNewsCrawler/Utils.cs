using System.Linq;
using HtmlAgilityPack;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Net;

namespace IndianNewsCrawler
{
    public class Utils
    {
        static Utils _instance = null;

        public static Utils Instance { get { if (_instance == null) { _instance = new Utils(); } return _instance; } }

        private Utils() { }

        public HtmlNode GetNodeOfAClass(HtmlDocument doc, string descendentType, string classname)
        {
            return doc.DocumentNode.Descendants(descendentType)
                    .First(node => node.Attributes.Contains("class") && node.Attributes["class"].Value == classname);
        }

        public string RelativeToAbsoluteUri(string pageUrl, string relativePath)
        {
            Uri WebsiteImAt = new Uri(pageUrl);
            return new Uri(WebsiteImAt, relativePath).AbsoluteUri.ToString();
        }

        internal List<string> TemplateByClassAndImgTag(HtmlDocument doc, string className)
        {
            var imageArray = new List<string>();
            try
            {
                var imageDiv = doc.DocumentNode.Descendants("div").First(x => x.Attributes.Contains("class") && x.Attributes["class"].Value == className);
                var imageUrl = imageDiv.Descendants("img").First(x => x.Attributes.Contains("src")).Attributes["src"].Value;
                imageArray.Add(imageUrl);
            }
            catch (Exception)
            {
            }
            return imageArray;
        }

        internal string GetRealUrl(string url)
        {            
            WebRequest request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Head;
            WebResponse response = request.GetResponse();
            var realUrl = response.ResponseUri.ToString();
            Console.WriteLine("Real Url: {0}", realUrl);
            return realUrl;
        }
    }


/// <summary>
/// Methods to remove HTML from strings.
/// </summary>
public static class HtmlRemoval
    {
        /// <summary>
        /// Remove HTML from string with Regex.
        /// </summary>
        public static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        /// <summary>
        /// Compiled regular expression for performance.
        /// </summary>
        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTagsRegexCompiled(string source)
        {
            return _htmlRegex.Replace(source, string.Empty);
        }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        public static string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
    }
}
