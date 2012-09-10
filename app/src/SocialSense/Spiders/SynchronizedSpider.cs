namespace SocialSense.Spiders
{
    public class SynchronizedSpider : Spider
    {
        private readonly object spiderLock = new object();

        public override string DownloadContent(string url)
        {
            lock (this.spiderLock)
            {
                return base.DownloadContent(url);
            }
        }
    }
}
