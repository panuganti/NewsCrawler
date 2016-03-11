using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerContracts
{
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
    public class Feed
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Link { get; set; }
        [DataMember]
        public DateTime PublishedDate { get; set; }
    }

    [DataContract]
    public class ShortStory : Feed
    {
        [DataMember]
        public string Guid { get; set; }
        [DataMember]
        public string ImageUrl { get; set; }
        [DataMember]
        public Source Source { get; set; }
    }
    
    [DataContract]
    public enum Source
    {
        [EnumMember(Value = "TOI")]
        TOI,
        [EnumMember(Value = "PTI")]
        PTI,
        [EnumMember(Value = "NDTV")]
        NDTV
    }
}
