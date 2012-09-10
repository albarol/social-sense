namespace SocialSense.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    using SocialSense.Parsers.Mapping;
    using SocialSense.Shared;

    public class FacebookParser : IParser
    {
        private const string AcceptType = "status";

        public SearchResult Parse(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException("content not be null -or- content not be empty");
            }

            var results = new List<ResultItem>();
            var facebookResult = this.GetFacebookResult(content);

            foreach (FacebookPost currentResult in facebookResult.Posts.Where(p => p.Type.Equals(AcceptType)))
            {
                var current = new ResultItem
                {
                    Date = currentResult.CreatedTime,
                    Url = string.Format("http://www.facebook.com/profile.php?id={0}", currentResult.Author.Id),
                    Author = currentResult.Author.Name,
                    Snippet = currentResult.Message ?? currentResult.Description,
                    Title = currentResult.Name
                };
                results.Add(current);
            }
            return new SearchResult { Items = results };
        }

        private FacebookMap GetFacebookResult(string content)
        {
            return JsonConvert.DeserializeObject<FacebookMap>(content.Replace("display(", string.Empty).Replace(");", string.Empty), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
