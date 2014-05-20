namespace SocialSense.Tests.Unit.Spiders
{
    using System;

    using NUnit.Framework;

	using FluentAssertions;

    using SocialSense.Spiders;
    using SocialSense.Spiders.Behaviors;

    [TestFixture, Category("Spiders")]
    public class RandomDelayedSpiderFixture
    {
        private Spider spider;
        private DateTime initialDate;

        [SetUp]
        public void SetUp()
        {
            this.spider = new Spider();
            this.spider.AddBehavior(new RandomDelayBehavior(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(7)));
            this.spider.TimeOut = 8;
        }


        [Test, ExpectedException(typeof(ArgumentException))]
        public void Constructor_ThrowsExceptionWhenFinalIntervalLessThanInitialInterval()
        {
            this.spider = new Spider();
            this.spider.AddBehavior(new RandomDelayBehavior(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(1)));
        }


        [Test]
        public void DownloadString_CanReadContentWithDelay()
        {
            this.initialDate = DateTime.Now;
            this.spider.DownloadContent("http://www.uol.com.br");
            (DateTime.Now - this.initialDate).TotalSeconds.Should().BeGreaterOrEqualTo(2).And.BeLessOrEqualTo(9);
        }
    }
}
