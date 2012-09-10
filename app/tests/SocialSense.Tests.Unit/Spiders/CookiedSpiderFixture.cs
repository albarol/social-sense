namespace SocialSense.Tests.Unit.Spiders
{
    using NUnit.Framework;

    using SharpTestsEx;

    using SocialSense.Spiders;
    using SocialSense.Spiders.Behaviors;

    [TestFixture, Category("Spiders")]
    public class CookiedSpiderFixture
    {
        private Spider spider;

        [SetUp]
        public void SetUp()
        {
            this.spider = new Spider();
            this.spider.AddBehavior(new CookieBehavior("PREF", "cookie-information"));
        }

        [Test]
        public void DownloadString_CanReadContent()
        {
            var content = this.spider.DownloadContent("http://www.uol.com.br");
            content.Should().Not.Be.Null();
        }
    }
}
