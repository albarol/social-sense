namespace SocialSense.Engines.Configurations
{
    using SocialSense.Engines;
    using SocialSense.Parsers;
    using SocialSense.Spiders;
    using SocialSense.UrlBuilders;

    public class TwitterEngineConfiguration : IEngineConfiguration
    {
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
                return new Spider();
            }
        }
    }
}
