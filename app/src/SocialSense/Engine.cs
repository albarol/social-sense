using System;
using System.Collections.Generic;
using SocialSense.Providers;
using SocialSense.Providers.Google;
using SocialSense.Providers.Bing;
using SocialSense.Providers.Yahoo;
using SocialSense.Providers.GooglePlus;

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
		
		public static IFinder Bing()
        {
            return new BingFinder ();
        }

        public static IFinder GooglePlus()
        {
            return new GooglePlusFinder ();
        }
    }
}

