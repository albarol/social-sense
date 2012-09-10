namespace SocialSense.Shared
{
    using System.Collections.Generic;

    public class SearchResult
    {
        public SearchResult()
        {
            this.Items = new List<ResultItem>();
            this.Parameters = new Dictionary<string, string>();
        }

        public IList<ResultItem> Items { get; set; }
        public bool HasNextPage { get; set; }
        public IDictionary<string, string> Parameters { get; set; }

        public bool HasParameters
        {
            get
            {
                return this.Parameters.Count > 0;
            }
        }
    }
}
