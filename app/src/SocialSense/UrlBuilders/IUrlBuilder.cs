namespace SocialSense.UrlBuilders
{
    using SocialSense.Shared;

    public interface IUrlBuilder
    {
        string WithQuery(Query query);
    }
}
