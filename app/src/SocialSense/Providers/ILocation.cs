using SocialSense.Shared;

namespace SocialSense.Providers
{
    public interface ILocation
    {
        string GetCountry(Country country);
        string GetLanguage(Language language);
    }
}
