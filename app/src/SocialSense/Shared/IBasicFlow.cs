using System;
using System.Collections.Generic;

namespace SocialSense.Shared
{
	public interface IBasicFlow
    {
		void Search (Query query, Action<IList<ResultItem>> results);
    }
}

