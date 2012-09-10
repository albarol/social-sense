namespace SocialSense.Shared
{
    using System;

    public struct ResultItem
    {
        public DateTime Date { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }
        public string Snippet { get; set; }
        public string Title { get; set; }
    }
}
