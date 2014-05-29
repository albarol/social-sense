using System;
using SocialSense.Shared;
using System.Collections.Generic;

namespace SocialSense
{
	public interface IFinder
    {
		void Search(Query query, Action<IList<ResultItem>> callback);
    }
}