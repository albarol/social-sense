using System;
using System.Collections.Generic;
using SocialSense.Providers;
using SocialSense.Providers.Google;
using SocialSense.Providers.Yahoo;

namespace SocialSense
{
    public class Engine
    {
		public static IFinder GoogleSite()
		{
			return new GoogleSiteFinder ();
		}

        public static IFinder GoogleNews()
        {
            return new GoogleNewsFinder ();
        }

		public static IFinder YahooNews()
		{
			return new YahooNewsFinder ();
		}
    }
}

