namespace SocialSense.Engines.Configurations
{
    using SocialSense.Engines;
    using SocialSense.Parsers;
    using SocialSense.Spiders;
    using SocialSense.Spiders.Behaviors;
    using SocialSense.UrlBuilders;

    public class YahooEngineConfiguration : IEngineConfiguration
    {
        public IParser Parser
        {
            get
            {
                return new YahooParser();
            }
        }

        public IUrlBuilder UrlBuilder
        {
            get
            {
                return new YahooUrlBuilder();
            }
        }

        public Spider Spider
        {
            get
            {
                var spider = new Spider();
                spider.AddBehavior(new RandomUserAgentBehavior());
                return spider;
            }
        }
    }
}