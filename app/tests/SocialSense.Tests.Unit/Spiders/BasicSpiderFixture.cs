namespace SocialSense.Tests.Unit.Spiders
{
    using System;

    using NUnit.Framework;

    using SharpTestsEx;

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
            content.Should().Not.Be.Null();
        }

        [Test, ExpectedException(typeof(UriFormatException))]
        public void DownloadString_ThrowsExceptionWhenUrlIsInvalid()
        {
            this.spider.DownloadContent("yahoo");
        }
    }
}
