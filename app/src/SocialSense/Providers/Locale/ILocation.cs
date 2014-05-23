namespace SocialSense.Providers.Locale
{
    using SocialSense.Shared;

    public interface ILocation
    {
        string GetCountry(Country country);

        string GetLanguage(Language language);
    }
}
