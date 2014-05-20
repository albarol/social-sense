namespace SocialSense.Tests.Unit.Spiders
{
    using System;

    using Moq;

    using NUnit.Framework;

	using FluentAssertions;

    using SocialSense.Spiders;

    [TestFixture, Category("Spiders")]
    public class SpiderFixture
    {
        private Mock<Spider> spider;

        [SetUp]
        public void SetUp()
        {
            this.spider = new Mock<Spider>();
        }

        [Test]
        public void DownloadString_CanReadContent()
        {
            this.spider.Setup(s => s.DownloadContent(It.IsAny<string>())).Returns("content");
            var content = this.spider.Object.DownloadContent("http://www.uol.com.br");
            content.Should().NotBeNull();
        }

        [Test, ExpectedException(typeof(UriFormatException))]
        public void DownloadString_ThrowsExceptionWhenUrlIsInvalid()
        {
            this.spider.Setup(s => s.DownloadContent(It.IsAny<string>())).Throws<UriFormatException>();
            this.spider.Object.DownloadContent("yahoo");
        }
    }
}
