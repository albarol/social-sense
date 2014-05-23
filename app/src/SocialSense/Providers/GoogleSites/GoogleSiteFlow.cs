using System;
using SocialSense.Shared;
using System.Collections.Generic;
using SocialSense.Network;
using System.Net;

namespace SocialSense.Providers.GoogleSites
{
	public class GoogleSiteFlow : IBasicFlow
    {
		private readonly RequestManager requester;
		private readonly GoogleSiteUrlBuilder urlBuilder;
		private readonly GoogleSiteParser parser;

		public GoogleSiteFlow()
		{
			this.requester = new RequestManager ();
			this.urlBuilder = new GoogleSiteUrlBuilder ();
			this.parser = new GoogleSiteParser ();
		}

		public void Search (Query query, Action<IList<ResultItem>> callback)
		{
			HttpRequest request = new HttpRequest (this.urlBuilder.WithQuery (query));
			int totalResults = 0;
			this.requester.Execute (request, (response) => {
				if (response.StatusCode == HttpStatusCode.OK) {
					SearchResult result = this.parser.Parse (response.Content);
					totalResults += result.Items.Count;
					callback(result.Items);

					if (result.HasNextPage && totalResults < query.MaxResults)
					{
						query.Page++;
						Search(query, callback);
					}
				}
			});
		}
    }
}

