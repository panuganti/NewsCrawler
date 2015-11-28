using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace NewsServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "NewsService" in both code and config file together.
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class NewsService : INewsService
    {
        [WebInvoke(Method = "GET", UriTemplate = "/GetMessage/{name}", ResponseFormat = WebMessageFormat.Json)]
        public string GetMessage(string name)
        {
            return name;
        }
    }
}
