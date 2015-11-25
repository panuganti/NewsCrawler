namespace IndianNewsCrawler
{
    public class Article
    {
        public string Title { get; set; }
        public string ArticleText { get; set; }
        public Source Source { get; set; }
    }

    public class RepImage
    {
        public string ImageUrl { get; set; }
        public string Caption { get; set; }
        public Source Source { get; set; }
    }

    public enum Source
    {
        TOI,
        PTI
    }
}
