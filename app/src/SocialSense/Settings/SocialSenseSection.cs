using System;
using System.Configuration;

namespace SocialSense.Settings
{
    public class SocialSenseSection : ConfigurationSection
    {
        public static SocialSenseSection GetSection()
        {
            return (SocialSenseSection)ConfigurationManager.GetSection("socialSense");
        }

        [ConfigurationProperty("bing")]
        public BingElement Bing
        {
            get
            {
                return (BingElement)this ["bing"];
            }
        }

        [ConfigurationProperty("googlePlus")]
        public GooglePlusElement GooglePlus
        {
            get
            {
                return (GooglePlusElement)this ["googlePlus"];
            }
        }
    }
}

