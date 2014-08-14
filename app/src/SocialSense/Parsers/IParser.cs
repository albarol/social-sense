namespace SocialSense.Parsers
{
    using SocialSense.Shared;

    public interface IParser
    {
        ParserResult Parse(string content);
    }
}
