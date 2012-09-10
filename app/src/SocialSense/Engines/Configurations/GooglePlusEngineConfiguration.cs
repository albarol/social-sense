namespace SocialSense.Engines.Configurations
{
    using SocialSense.Engines;
    using SocialSense.Parsers;
    using SocialSense.Spiders;
    using SocialSense.Spiders.Behaviors;
    using SocialSense.UrlBuilders;

    public class GooglePlusEngineConfiguration : IEngineConfiguration
    {
        private readonly string apiKey;

        public GooglePlusEngineConfiguration(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public IParser Parser
        {
            get
            {
                return new GooglePlusParser();
            }
        }

        public IUrlBuilder UrlBuilder
        {
            get
            {
                return new GooglePlusUrlBuilder(this.apiKey);
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
