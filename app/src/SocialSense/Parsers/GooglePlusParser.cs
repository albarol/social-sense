namespace SocialSense.Parsers
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using SocialSense.Parsers.Mapping;
    using SocialSense.Shared;

    public class GooglePlusParser : IParser
    {
        public SearchResult Parse(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException("content not be null -or- content not be empty");
            }
            
            var queryResults = JsonConvert.DeserializeObject<GooglePlusMap>(content);
            IList<ResultItem> results = new List<ResultItem>();
            foreach (ItemResult currentResult in queryResults.Items)
            {
                if (currentResult.Object.Type.Equals("note") && !string.IsNullOrEmpty(currentResult.Object.Content))
                {
                    var current = new ResultItem
                    {
                        Date = currentResult.Published,
                        Url = currentResult.Url,
                        Author = currentResult.Actor.DisplayName,
                        Snippet = currentResult.Object.Content,
                        Title = currentResult.Title
                    };
                    results.Add(current);
                }
            }

            return new SearchResult
            {
                Items = results,
                HasNextPage = !string.IsNullOrEmpty(queryResults.NextPageToken),
                Parameters = new Dictionary<string, string> { { "pageToken", queryResults.NextPageToken } }
            };
        }
    }
}
