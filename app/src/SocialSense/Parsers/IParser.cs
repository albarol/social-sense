namespace SocialSense.Parsers
{
    using SocialSense.Shared;

    public interface IParser
    {
        SearchResult Parse(string content);
    }
}
