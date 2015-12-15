using System.ServiceModel;
using System.ServiceModel.Web;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.IO;

namespace NewsServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "NewsService" in both code and config file together.
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class NewsService : INewsService
    {
        [WebInvoke(Method = "GET", UriTemplate = "/GetMessage/{name}", 
                                ResponseFormat = WebMessageFormat.Json)]
        public string GetMessage(string name)
        {
            return name;
        }
        
        [WebInvoke(Method = "GET", UriTemplate = "/GetLatestNews/{profile}/{guid}", 
                                            ResponseFormat = WebMessageFormat.Json)]
        public IEnumerable<ShortStory> GetLatestNews(string profile, string guid)
        {
            var shortStories = new List<ShortStory>();
            var now = DateTime.Now;
            string ndtvFilename = String.Format("{0}_{1}_{2}.ndtv.json", now.Year, now.Month, now.Day);            
            var ndtvShortStories = JsonConvert.DeserializeObject<List<ShortStory>>(File.ReadAllText(ndtvFilename));
            string toiFilename = String.Format("{0}_{1}_{2}.toi.json", now.Year, now.Month, now.Day);            
            var toiShortStories = JsonConvert.DeserializeObject<List<ShortStory>>(File.ReadAllText(toiFilename));
            shortStories.AddRange(ndtvShortStories);
            shortStories.AddRange(toiShortStories);
            return shortStories;            
        }
    }
}
