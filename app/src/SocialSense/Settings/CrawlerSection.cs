using System;
using System.Configuration;

namespace SocialSense
{
    public class CrawlerSection : ConfigurationSection
    {
        public static CrawlerSection GetSection()
        {
            return (CrawlerSection)System.Configuration.ConfigurationManager.GetSection("crawlerSection") ?? new CrawlerSection();
        }

        [ConfigurationProperty("bing", IsRequired = false)]
        public BingElement Bing
        {
            get
            {
                return (BingElement)this ["bing"];
            }
            set
            {
                this ["bing"] = value;
            }
        }

        [ConfigurationProperty("googleplus", IsRequired = false)]
        public GooglePlusElement GooglePlus
        {
            get
            {
                return (GooglePlusElement)this ["googleplus"];
            }
            set
            {
                this ["googleplus"] = value;
            }
        }
    }
}

