namespace SocialSense.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using Newtonsoft.Json;

    using SocialSense.Parsers.Mapping;
    using SocialSense.Shared;

    public class TwitterParser : IParser
    {
        public SearchResult Parse(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException("content not be null -or- content not be empty");
            }

            var queryResults = JsonConvert.DeserializeObject<TwitterMap>(content);
            IList<ResultItem> results = new List<ResultItem>();
            foreach (TwittResult currentResult in queryResults.Results)
            {
                var current = new ResultItem
                {
                    Date = currentResult.CreatedAt,
                    Url = string.Format("http://www.twitter.com/{0}/statuses/{1}", currentResult.FromUser, currentResult.Id),
                    Author = currentResult.FromUser,
                    Snippet = currentResult.Text,
                    Title = currentResult.Title
                };
                results.Add(current);
            }

            return new SearchResult { Items = results, HasNextPage = !string.IsNullOrEmpty(queryResults.NextPage) };
        }
    }
}
