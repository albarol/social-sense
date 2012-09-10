namespace SocialSense.Tests.Unit.Spiders
{
    using System.Threading;

    using NUnit.Framework;

    using SocialSense.Spiders;

    [TestFixture, Category("Spiders")]
    public class SynchronizedSpiderFixture
    {
        private Spider spider;

        [SetUp]
        public void SetUp()
        {
            this.spider = new SynchronizedSpider();
        }

        [Test]
        public void DownloadString_CanReadContent()
        {
            var firstThread = new Thread(() => this.spider.DownloadContent("http://www.uol.com.br"));
            var secondThread = new Thread(() => this.spider.DownloadContent("http://www.uol.com.br"));
            var thirdThread = new Thread(() => this.spider.DownloadContent("http://www.uol.com.br"));
            
            firstThread.Start();
            secondThread.Start();
            thirdThread.Start();
        }
    }
}
