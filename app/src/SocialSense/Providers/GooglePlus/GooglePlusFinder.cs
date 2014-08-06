using System;
using SocialSense.Network;
using SocialSense.Shared;
using System.Collections.Generic;
using System.Net;

namespace SocialSense.Providers.GooglePlus
{
    public class GooglePlusFinder : IFinder
    {
        private readonly GooglePlusUrlBuilder urlBuilder;
        private readonly RequestManager requester;
        private readonly GooglePlusParser parser;

        public GooglePlusFinder ()
        {
            this.urlBuilder = new GooglePlusUrlBuilder ();
            this.requester = new RequestManager ();
            this.parser = new GooglePlusParser ();
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
                        query.Parameters = result.Parameters;
                        query.MinResults -= totalResults;
                        Search(query, callback);
                    }
                }
            });
        }
    }
}

