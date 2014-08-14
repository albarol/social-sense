using System;
using SocialSense.Providers;
using SocialSense.Network;
using SocialSense.Providers.Bing;
using SocialSense.Shared;
using System.Collections.Generic;
using System.Net;

namespace SocialSense.Providers.Bing
{
    public class BingFinder : IFinder
    {
        private readonly RequestManager requester;
        private readonly BingUrlBuilder urlBuilder;
        private readonly BingParser parser;

        public BingFinder()
        {
            this.requester = new RequestManager ();
            this.urlBuilder = new BingUrlBuilder ();
            this.parser = new BingParser ();
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

