namespace SocialSense.Tests.Unit.Urls
{
    using System;

    using NUnit.Framework;

	using FluentAssertions;

    using SocialSense.Shared;
    using SocialSense.UrlBuilders;

    [TestFixture, Category("UrlBuilder")]
    public class DiggUrlBuilderFixture
    {
        private IUrlBuilder builder;

        [SetUp]
        public void SetUp()
        {
            this.builder = new DiggUrlBuilder();
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void WithQuery_ThrowsExceptionWhenTermIsNullOrEmpty()
        {
            this.builder.WithQuery(new Query());
        }

        [TestCase("cultura", Result = "http://services.digg.com/2.0/search.search?count=100&media=news&query=cultura&offset=1")]
        [TestCase("cultura-v2", Result = "http://services.digg.com/2.0/search.search?count=100&media=news&query=cultura-v2&offset=1")]
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
            var resultUrl = this.builder.WithQuery(query);

            // Assert:
            var beginUnixTime = DateParser.ToUnixTimestamp(period.Begin);
            var endUnixTime = DateParser.ToUnixTimestamp(period.End);
            var comparableUrl = string.Format("http://services.digg.com/2.0/search.search?count=100&media=news&query=cultura&min_date={0}&max_date={1}&offset=1", beginUnixTime, endUnixTime);
            resultUrl.Should().Be(comparableUrl);
        }

        [Ignore("Digg does not support language parameter")]
        [TestCase(Language.Portuguese, Result = "")]
        [TestCase(Language.English, Result = "")]
        [TestCase(Language.Undefined, Result = "")]
        public string WithQuery_IncludeLanguageInQuery(Language language)
        {
            return this.builder.WithQuery(new Query { Term = "cultura", Language = language });
        }

        [Ignore("Digg does not support country parameter")]
        [TestCase(Country.Brazil, Result = "")]
        [TestCase(Country.Mexico, Result = "")]
        [TestCase(Country.Undefined, Result = "")]
        public string WithQuery_IncludeCountryInQuery(Country country)
        {
            return this.builder.WithQuery(new Query { Term = "cultura", Country = country });
        }


        [TestCase(1, Result = "http://services.digg.com/2.0/search.search?count=100&media=news&query=cultura&offset=1")]
        [TestCase(2, Result = "http://services.digg.com/2.0/search.search?count=100&media=news&query=cultura&offset=2")]
        public string WithQuery_IncludePageParameter(int pageNumber)
        {
            return this.builder.WithQuery(new Query { Term = "cultura", Page = pageNumber });
        }
    }
}
