namespace SocialSense.Spiders
{
    public interface ISpider
    {
        string DownloadContent(string url);
    }
}