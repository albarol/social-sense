namespace SocialSense.Engines.Configurations
{
    using SocialSense.Engines;
    using SocialSense.Parsers;
    using SocialSense.Spiders;
    using SocialSense.Spiders.Behaviors;
    using SocialSense.UrlBuilders;

    public class BingEngineConfiguration : IEngineConfiguration
    {
        public IParser Parser
        {
            get
            {
                return new BingParser();
            }
        }

        public IUrlBuilder UrlBuilder
        {
            get
            {
                return new BingUrlBuilder();
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
