namespace SocialSense.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    using Newtonsoft.Json;

    using SocialSense.Parsers.Mapping;
    using SocialSense.Shared;

    public class DiggParser : IParser
    {
        public SearchResult Parse(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException("content not be null -or- content not be empty");
            }
            var diggResult = JsonConvert.DeserializeObject<DiggMap>(content);

            var results = new List<ResultItem>();
            foreach (DiggStory currentResult in diggResult.Stories)
            {
                var current = new ResultItem
                {
                    Date = currentResult.CreatedTime,
                    Url = currentResult.Link,
                    Author = currentResult.User.Name,
                    Snippet = currentResult.Description,
                    Title = currentResult.Title
                };
                results.Add(current);
            }

            return new SearchResult { Items = results };
        }
    }
}
