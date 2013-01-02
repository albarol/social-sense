namespace SocialSense.Engines.Configurations
{
    using SocialSense.Engines;
    using SocialSense.Parsers;
    using SocialSense.Shared;
    using SocialSense.Spiders;
    using SocialSense.Spiders.Behaviors;
    using SocialSense.UrlBuilders;

    public class GoogleNewsEngineConfiguration : IEngineConfiguration
    {
        public IParser Parser
        {
            get
            {
                return new GoogleNewsParser();
            }
        }

        public IUrlBuilder UrlBuilder
        {
            get
            {
                return new GoogleUrlBuilder(GoogleSource.News);
            }
        }

        public Spider Spider
        {
            get
            {
                var spider = new Spider();
                spider.AddBehavior(new GoogleUserAgentBehavior());
                return spider;
            }
        }
    }
}
