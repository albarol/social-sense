namespace SocialSense.Tests.Unit.Urls
{
    using System;

    using NUnit.Framework;

	using FluentAssertions;

    using SocialSense.Shared;
    using SocialSense.UrlBuilders;

    [TestFixture, Category("UrlBuilder")]
    public class FacebookUrlBuilderFixture
    {
        private IUrlBuilder builder;

        [SetUp]
        public void SetUp()
        {
            this.builder = new FacebookUrlBuilder();
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void WithQuery_ThrowsExceptionWhenTermIsNullOrEmpty()
        {
            this.builder.WithQuery(new Query());
        }

        [TestCase("cultura", Result = "https://graph.facebook.com/search?type=post&metadata=1&callback=display&limit=50&q=cultura&offset=0")]
        [TestCase("cultura-v2", Result = "https://graph.facebook.com/search?type=post&metadata=1&callback=display&limit=50&q=cultura-v2&offset=0")]
        public string WithQuery_IncludeTermInQuery(string term)
        {
            return this.builder.WithQuery(new Query { Term = term });
        }

        [Test]
        public void WithQuery_IncludePeriodInQuery()
        {
            // Arrange:
            var period = Period.Today;
            var query = new Query { Term = "cultura", Period = period };

            // Act:
            var url = this.builder.WithQuery(query);

            // Assert:
            url.Should().Be(string.Format("https://graph.facebook.com/search?type=post&metadata=1&callback=display&limit=50&q=cultura&since={0}&offset=0", period.Begin.ToString("yyyyMMdd")));
        }

        [TestCase(Language.Portuguese, Country.Brazil, Result = "https://graph.facebook.com/search?type=post&metadata=1&callback=display&limit=50&q=cultura&locate=pt_br&offset=0")]
        [TestCase(Language.English, Country.UnitedStatesOfAmerica, Result = "https://graph.facebook.com/search?type=post&metadata=1&callback=display&limit=50&q=cultura&locate=en_us&offset=0")]
        [TestCase(Language.Undefined, Country.Undefined, Result = "https://graph.facebook.com/search?type=post&metadata=1&callback=display&limit=50&q=cultura&offset=0")]
        public string WithQuery_IncludeLanguageAndCountryInQuery(Language language, Country country)
        {
            return this.builder.WithQuery(new Query { Term = "cultura", Language = language, Country = country });
        }

        [TestCase(1, Result = "https://graph.facebook.com/search?type=post&metadata=1&callback=display&limit=50&q=cultura&offset=0")]
        [TestCase(2, Result = "https://graph.facebook.com/search?type=post&metadata=1&callback=display&limit=50&q=cultura&offset=100")]
        public string WithQuery_IncludePageParameter(int pageNumber)
        {
            return this.builder.WithQuery(new Query { Term = "cultura", Page = pageNumber });
        }
    }
}
