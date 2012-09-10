namespace SocialSense.Shared
{
    using System.Collections.Generic;

    public class Query
    {
        private int maxResults;
        private int page;

        public Query()
        {
            this.Parameters = new Dictionary<string, string>();
        }

        public string Term { get; set; }
        public Period? Period { get; set; }
        public Language? Language { get; set; }
        public Country? Country { get; set; }
        public IDictionary<string, string> Parameters { get; set; }
        public int Page
        {
            get
            {
                if (this.page <= 0)
                {
                    this.page = 1;
                }

                return this.page;
            }

            set
            {
                this.page = value;
            }
        }

        public int MaxResults
        {
            get
            {
                return this.maxResults == 0 ? 400 : this.maxResults;
            }

            set
            {
                if (value >= 1)
                {
                    this.maxResults = value;
                }
            }
        }
    }
}