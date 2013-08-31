using SocialSense.Authorization;

namespace SocialSense.Engines.Configurations
{
    using SocialSense.Engines;
    using SocialSense.Parsers;
    using SocialSense.Spiders;
    using SocialSense.Spiders.Behaviors;
    using SocialSense.UrlBuilders;

    public class FacebookEngineConfiguration : IEngineConfiguration
    {
        private IAuthorization authorization;

        public FacebookEngineConfiguration(IAuthorization authorization)
        {
            this.authorization = authorization;
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
                return new FacebookUrlBuilder();
            }
        }

        public Spider Spider
        {
            get
            {
                var spider = new Spider();
                spider.AddBehavior(new AuthorizationBehavior(this.authorization));
                spider.AddBehavior(new RandomUserAgentBehavior());
                return spider;
            }
        }
    }
}
