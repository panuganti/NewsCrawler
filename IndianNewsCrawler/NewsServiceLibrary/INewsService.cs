using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace NewsServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "INewsService" in both code and config file together.
    [ServiceContract]
    public interface INewsService
    {
        [OperationContract]
        string GetMessage(string name);
        [OperationContract]
        IEnumerable<ShortStory> GetLatestNews(string profile, string guid);

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
        [EnumMember]
        TOI,
        [EnumMember]
        PTI,
        [EnumMember]
        NDTV
    }
}
