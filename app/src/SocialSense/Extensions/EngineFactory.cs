using SocialSense.Authorization;

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

        public static Engine Facebook(FacebookAuthorization authorization)
        {
            return new Engine(new FacebookEngineConfiguration(authorization));
        }

        public static Engine GoogleNews()
        {
            return new Engine(new GoogleNewsEngineConfiguration());
        }

        public static Engine GoogleSites()
        {
            return new Engine(new GoogleEngineConfiguration());
        }

        public static Engine GooglePlus(GooglePlusAuthorization authorization)
        {
            return new Engine(new GooglePlusEngineConfiguration(authorization));
        }

        public static Engine Twitter(TwitterAuthorization authorization)
        {
            return new Engine(new TwitterEngineConfiguration(authorization));
        }

        public static Engine Yahoo()
        {
            return new Engine(new YahooEngineConfiguration());
        }
    }
}
