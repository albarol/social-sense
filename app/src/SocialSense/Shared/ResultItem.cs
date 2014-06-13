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

		public override string ToString ()
		{
			return string.Format ("[ResultItem: Date={0}, Url={1}, Author={2}, Snippet={3}, Title={4}]", Date, Url, Author, Snippet, Title);
		}
    }
}
