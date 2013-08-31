using SocialSense.Authorization;

namespace SocialSense.Engines.Configurations
{
    using SocialSense.Engines;
    using SocialSense.Parsers;
    using SocialSense.Spiders;
    using SocialSense.Spiders.Behaviors;
    using SocialSense.UrlBuilders;

    public class GooglePlusEngineConfiguration : IEngineConfiguration
    {
        private IAuthorization authorization;

        public GooglePlusEngineConfiguration(IAuthorization authorization)
        {
            this.authorization = authorization;
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
                return new GooglePlusUrlBuilder();
            }
        }

        public Spider Spider
        {
            get
            {
                var spider = new Spider();
                spider.AddBehavior(new AuthorizationBehavior(authorization));
                spider.AddBehavior(new RandomUserAgentBehavior());
                return spider;
            }
        }
    }
}
