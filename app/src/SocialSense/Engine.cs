using System;
using System.Collections.Generic;
using SocialSense.Providers.GoogleSites;

namespace SocialSense
{
    public class Engine
    {
		public static IFinder GoogleSite()
		{
			return new GoogleSiteFinder ();
		}
    }
}

