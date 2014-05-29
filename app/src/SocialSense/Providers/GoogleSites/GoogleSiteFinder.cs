using System;
using SocialSense.Shared;
using System.Collections.Generic;
using SocialSense.Network;
using System.Net;

namespace SocialSense.Providers.GoogleSites
{
	public class GoogleSiteFinder : IFinder
    {
		private readonly RequestManager requester;
		private readonly GoogleSiteUrlBuilder urlBuilder;
		private readonly GoogleSiteParser parser;

		public GoogleSiteFinder()
		{
			this.requester = new RequestManager ();
			this.urlBuilder = new GoogleSiteUrlBuilder ();
			this.parser = new GoogleSiteParser ();
		}

		public void Search (Query query, Action<IList<ResultItem>> callback)
		{
			HttpRequest request = PrepareRequest (query);
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

		private HttpRequest PrepareRequest(Query query)
		{
			Random rnd = new Random ();
			string[] userAgents = new string[] {
				"( Robots.txt Validator http://www.searchengineworld.com/cgi-bin/robotcheck.cgi )",
				"Mozilla/4.0 (compatible; MSIE 5.0; Windows 95) VoilaBot BETA 1.2 (http://www.voila.com/)"
			};
			HttpRequest request = new HttpRequest (this.urlBuilder.WithQuery (query));
			request.UserAgent = userAgents[rnd.Next(0, userAgents.Length - 1)];
			return request;
		}
    }
}

