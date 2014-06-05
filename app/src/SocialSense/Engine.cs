using System;
using System.Collections.Generic;
using SocialSense.Providers.Google;
using SocialSense.Providers;
using SocialSense.Providers.Bing;

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

        public static IFinder Bing()
        {
            return new BingFinder ();
        }
    }
}

