using System;
using SocialSense.Shared;
using System.Collections.Generic;
using SocialSense.Network;
using System.Net;

namespace SocialSense.Providers.Yahoo
{
	public class YahooNewsFinder : IFinder
    {
		private readonly RequestManager requester;
		private readonly YahooUrlBuilder urlBuilder;
		private readonly YahooNewsParser parser;

		public YahooNewsFinder()
		{
			this.requester = new RequestManager ();
			this.urlBuilder = new YahooUrlBuilder ();
			this.parser = new YahooNewsParser ();
		}

        public void Search (Query query, Action<IList<ResultItem>> callback)
		{
			HttpRequest request = new HttpRequest(this.urlBuilder.WithQuery(query));
			this.requester.Execute (request, (response) => {
				if (response.StatusCode == HttpStatusCode.OK) {
					SearchResult result = this.parser.Parse (response.Content);
					int totalResults = result.Items.Count;
					callback(result.Items);

                    if (result.HasNextPage && totalResults < query.MinResults)
					{
						query.Page++;
						query.MinResults -= totalResults;
						Search(query, callback);
					}
				}
			});
		}
    }
}

