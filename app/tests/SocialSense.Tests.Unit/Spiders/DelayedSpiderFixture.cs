namespace SocialSense.Tests.Unit.Spiders
{
    using System;

    using NUnit.Framework;

    using SharpTestsEx;

    using SocialSense.Spiders;
    using SocialSense.Spiders.Behaviors;

    [TestFixture, Category("Spiders")]
    public class DelayedSpiderFixture
    {
        private Spider spider;
        private DateTime initialDate;

        [SetUp]
        public void SetUp()
        {
            this.spider = new Spider();
            this.spider.AddBehavior(new DelayBehavior(TimeSpan.FromSeconds(10)));
            this.spider.TimeOut = 8;
        }

        [Test]
        public void DownloadString_CanReadContentWithDelay()
        {
            this.initialDate = DateTime.Now;
            this.spider.DownloadContent("http://www.uol.com.br");
            (DateTime.Now - this.initialDate).TotalSeconds.Should().Be.GreaterThanOrEqualTo(8);
        }
    }
}
