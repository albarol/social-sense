using System;
using SocialSense.Network;
using SocialSense.Shared;
using SocialSense.UrlBuilders;
using SocialSense.Providers.Google;
using System.Collections.Generic;

namespace SocialSense.Providers.Google
{
    public abstract class BaseGoogleFinder : IFinder
    {
        public abstract void Search (Query query, Action<IList<ResultItem>> successCallback);
        public abstract void Search (Query query, Action<IList<ResultItem>> successCallback, Action<HttpResponse> errorCallback);

        protected HttpRequest PrepareRequest(string url)
        {
            Random rnd = new Random ();
            string[] userAgents = new string[] {
                "( Robots.txt Validator http://www.searchengineworld.com/cgi-bin/robotcheck.cgi )",
                "Mozilla/4.0 (compatible; MSIE 5.0; Windows 95) VoilaBot BETA 1.2 (http://www.voila.com/)"
            };
            HttpRequest request = new HttpRequest (url);
            request.UserAgent = userAgents[rnd.Next(0, userAgents.Length - 1)];
            return request;
        }
    }
}

