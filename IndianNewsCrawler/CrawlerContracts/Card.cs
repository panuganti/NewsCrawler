using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerContracts
{
    [DataContract]
    public class Card
    {
        [DataMember]
        public string Guid { get; set; }
        [DataMember]
        public string ShortStoryId { get; set; }
        [DataMember]
        public string ExpandedCardId { get; set; }
        [DataMember]
        public string SocialCardId { get; set; }
    }

    [DataContract]
    public class SocialCard
    {
        [DataMember]
        public string Guid { get; set; }
        [DataMember]
        public int Likes { get; set; }
        [DataMember]
        public int Shares { get; set; }
    }

    [DataContract]
    public class ShortStory
    {
        [DataMember]
        public string Guid { get; set; }
        [DataMember]
        public string ImageUrl { get; set; }
        [DataMember]
        public string Headline { get; set; }
        [DataMember]
        public string Summary { get; set; }
        [DataMember]
        public string Source { get; set; }        
    }

    [DataContract]
    public class ExpandedCard
    {
        public string Guid { get; set; }
        public string Headline { get; set; }
        public string ImageUrl { get; set; }
        public string Body { get; set; }
    }

}
