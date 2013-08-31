namespace SocialSense.Extensions
{
    using SocialSense.Engines;
    using SocialSense.Engines.Configurations;

    public static class EngineFactory
    {
        public static Engine Bing()
        {
            return new Engine(new BingEngineConfiguration());
        }

        public static Engine Digg()
        {
            return new Engine(new DiggEngineConfiguration());
        }

        public static Engine Facebook(string accessToken)
        {
            return new Engine(new FacebookEngineConfiguration(accessToken));
        }

        public static Engine GoogleNews()
        {
            return new Engine(new GoogleNewsEngineConfiguration());
        }

        public static Engine GoogleSites()
        {
            return new Engine(new GoogleEngineConfiguration());
        }

        public static Engine GooglePlus(string apiKey)
        {
            return new Engine(new GooglePlusEngineConfiguration(apiKey));
        }

        public static Engine Twitter()
        {
            return new Engine(new TwitterEngineConfiguration());
        }

        public static Engine Yahoo()
        {
            return new Engine(new YahooEngineConfiguration());
        }
    }
}
