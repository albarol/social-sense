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
            Search(query, callback, ((HttpResponse obj) => {}));
        }

        public void Search (Query query, Action<IList<ResultItem>> successCallback, Action<HttpResponse> errorCallback)
		{
			HttpRequest request = new HttpRequest(this.urlBuilder.WithQuery(query));
			this.requester.Execute (request, (response) => {
				if (response.StatusCode == HttpStatusCode.OK) {
					ParserResult result = this.parser.Parse (response.Content);
					int totalResults = result.Items.Count;
                    successCallback(result.Items);

                    if (result.HasNextPage && totalResults < query.MinResults)
					{
						query.Page++;
						query.MinResults -= totalResults;
                        Search(query, successCallback);
					}
				}
                else {
                    errorCallback(response);
                }
			});
		}
    }
}

