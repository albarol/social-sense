namespace SocialSense.Engines.Configurations
{
    using SocialSense.Engines;
    using SocialSense.Parsers;
    using SocialSense.Spiders;
    using SocialSense.Spiders.Behaviors;
    using SocialSense.UrlBuilders;

    public class FacebookEngineConfiguration : IEngineConfiguration
    {
        private string accessToken;

        public FacebookEngineConfiguration(string accessToken)
        {
            this.accessToken = accessToken;
        }

        public IParser Parser
        {
            get
            {
                return new FacebookParser();
            }
        }

        public IUrlBuilder UrlBuilder
        {
            get
            {
                return new FacebookUrlBuilder(this.accessToken);
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
