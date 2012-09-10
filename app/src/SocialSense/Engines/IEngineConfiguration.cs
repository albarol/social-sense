namespace SocialSense.Engines
{
    using SocialSense.Parsers;
    using SocialSense.Spiders;
    using SocialSense.UrlBuilders;

    public interface IEngineConfiguration
    {
        IParser Parser { get; }
        IUrlBuilder UrlBuilder { get; }
        Spider Spider { get; }
    }
}
