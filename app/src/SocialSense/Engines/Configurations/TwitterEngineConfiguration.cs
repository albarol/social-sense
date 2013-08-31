using SocialSense.Authorization;
using SocialSense.Spiders.Behaviors;

namespace SocialSense.Engines.Configurations
{
    using SocialSense.Engines;
    using SocialSense.Parsers;
    using SocialSense.Spiders;
    using SocialSense.UrlBuilders;

    public class TwitterEngineConfiguration : IEngineConfiguration
    {
        private IAuthorization authorization;

        public TwitterEngineConfiguration(TwitterAuthorization authorization)
        {
            this.authorization = authorization;
        }
        
        public IParser Parser
        {
            get
            {
                return new TwitterParser();
            }
        }

        public IUrlBuilder UrlBuilder
        {
            get
            {
                return new TwitterUrlBuilder();
            }
        }

        public Spider Spider
        {
            get
            {
                var spider = new Spider();
                spider.AddBehavior(new AuthorizationBehavior(this.authorization));
                return spider;
            }
        }
    }
}
