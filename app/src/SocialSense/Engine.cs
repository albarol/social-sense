using System;
using System.Collections.Generic;
using SocialSense.Providers.GoogleSites;

namespace SocialSense
{
    public class Engine
    {
		public static IBasicFlow GoogleSite()
		{
			return new GoogleSiteFlow ();
		}
    }
}

