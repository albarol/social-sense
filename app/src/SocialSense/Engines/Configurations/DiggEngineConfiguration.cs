namespace SocialSense.Engines.Configurations
{
    using System;

    using SocialSense.Engines;
    using SocialSense.Parsers;
    using SocialSense.Spiders;
    using SocialSense.Spiders.Behaviors;
    using SocialSense.UrlBuilders;

    public class DiggEngineConfiguration : IEngineConfiguration
    {
        public DiggEngineConfiguration()
        {
            throw new NotSupportedException();
        }
        
        public IParser Parser
        {
            get
            {
                return new DiggParser();
            }
        }

        public IUrlBuilder UrlBuilder
        {
            get
            {
                return new DiggUrlBuilder();
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
