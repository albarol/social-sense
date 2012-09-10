namespace SocialSense.Engines
{
    using System.Collections.Generic;
    using System.Linq;

    using SocialSense.Parsers;
    using SocialSense.Shared;
    using SocialSense.Spiders;
    using SocialSense.UrlBuilders;

    public class Engine
    {
        private readonly IParser parser;
        private readonly IUrlBuilder urlBuilder;
        private readonly Spider spider;

        public Engine(IEngineConfiguration configuration)
        {
            this.parser = configuration.Parser;
            this.urlBuilder = configuration.UrlBuilder;
            this.spider = configuration.Spider;
        }

        public IList<ResultItem> Search(Query query)
        {
            var results = new List<ResultItem>();
            SearchResult searchResult;
            do
            {
                try
                {
                    string url = this.urlBuilder.WithQuery(query);
                    string content = this.spider.DownloadContent(url);
                    searchResult = this.parser.Parse(content);
                    results.AddRange(searchResult.Items);

                    if (searchResult.HasParameters)
                    {
                        query.Parameters = searchResult.Parameters;
                    }
                }
                catch
                {
                    break;
                }
                finally
                {
                    query.Page++;
                }
            }
            while (searchResult.HasNextPage && results.Count < query.MaxResults);
            return results.Take(query.MaxResults).ToList();
        }
    }
}
