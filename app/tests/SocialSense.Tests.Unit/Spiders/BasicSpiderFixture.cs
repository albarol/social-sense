namespace SocialSense.Tests.Unit.Spiders
{
    using System;

    using NUnit.Framework;

	using FluentAssertions;

    using SocialSense.Spiders;

    [TestFixture, Category("Spiders")]
    public class BasicSpiderFixture
    {
        private Spider spider;

        [SetUp]
        public void SetUp()
        {
            this.spider = new Spider();
        }

        [Test]
        public void DownloadString_CanReadContent()
        {
            var content = this.spider.DownloadContent("http://www.uol.com.br");
			content.Should().NotBeNull();
        }

        [Test, ExpectedException(typeof(UriFormatException))]
        public void DownloadString_ThrowsExceptionWhenUrlIsInvalid()
        {
            this.spider.DownloadContent("yahoo");
        }
    }
}
