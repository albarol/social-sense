namespace SocialSense.UrlBuilders.Parameters
{
    using SocialSense.Shared;

    public interface ILocation
    {
        string GetCountry(Country country);

        string GetLanguage(Language language);
    }
}
