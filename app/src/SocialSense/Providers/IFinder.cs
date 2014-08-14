using System;
using SocialSense.Shared;
using System.Collections.Generic;
using SocialSense.Network;

namespace SocialSense.Providers
{
	public interface IFinder
    {
        void Search(Query query, Action<IList<ResultItem>> successCallback);
        void Search(Query query, Action<IList<ResultItem>> successCallback, Action<HttpResponse> errorCallback);
    }
}