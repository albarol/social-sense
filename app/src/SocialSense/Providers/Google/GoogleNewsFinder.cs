using System;
using SocialSense.Network;
using SocialSense.Shared;
using System.Collections.Generic;
using System.Net;

namespace SocialSense.Providers.Google
{
    public class GoogleNewsFinder : BaseGoogleFinder
    {
        private readonly RequestManager requester;
        private readonly GoogleUrlBuilder urlBuilder;
        private readonly GoogleNewsParser parser;

        public GoogleNewsFinder()
        {
            this.requester = new RequestManager ();
            this.urlBuilder = new GoogleUrlBuilder (GoogleSource.News);
            this.parser = new GoogleNewsParser ();
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

        public override void Search (Query query, Action<IList<ResultItem>> callback)
        {
            Search(query, callback, ((HttpResponse obj) => {}));
        }
    }
}

