using System;
using SocialSense.Shared;
using System.Collections.Generic;
using SocialSense.Network;
using System.Net;

namespace SocialSense.Providers.Google
{
    public class GoogleSiteFinder : BaseGoogleFinder
    {
		private readonly RequestManager requester;
        private readonly GoogleUrlBuilder urlBuilder;
        private readonly GoogleSiteParser parser;

		public GoogleSiteFinder()
		{
			this.requester = new RequestManager ();
            this.urlBuilder = new GoogleUrlBuilder (GoogleSource.Site);
            this.parser = new GoogleSiteParser ();
		}

        public override void Search (Query query, Action<IList<ResultItem>> callback)
		{
            Search(query, callback, ((HttpResponse obj) => {}));
		}

        public override void Search (Query query, Action<IList<ResultItem>> successCallback, Action<HttpResponse> errorCallback) 
        {
            HttpRequest request = PrepareRequest (this.urlBuilder.WithQuery(query));
            this.requester.Execute (request, (response) => {
                if (response.StatusCode == HttpStatusCode.OK) {
                    ParserResult result = this.parser.Parse (response.Content);
                    int totalResults = result.Items.Count;
                    successCallback(result.Items);

                    if (result.HasNextPage && totalResults < query.MinResults)
                    {
                        query.Page++;
                        query.MinResults -= totalResults;
                        Search(query, successCallback, errorCallback);
                    }
                }
                else {
                    errorCallback(response);
                }
            });

        }
    }
}

