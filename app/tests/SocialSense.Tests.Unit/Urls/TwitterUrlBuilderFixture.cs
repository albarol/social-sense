namespace SocialSense.Tests.Unit.Urls
{
    using System;

    using NUnit.Framework;

	using FluentAssertions;

    using SocialSense.Shared;
    using SocialSense.UrlBuilders;

    [TestFixture, Category("UrlBuilder")]
    public class TwitterUrlBuilderFixture
    {
        private IUrlBuilder builder;

        [SetUp]
        public void SetUp()
        {
            this.builder = new TwitterUrlBuilder();
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void WithQuery_ThrowsExceptionWhenTermIsNullOrEmpty()
        {
            this.builder.WithQuery(new Query());
        }

        [TestCase("cultura", Result = "https://api.twitter.com/1.1/search/tweets.json?rpp=100&result_type=recent&q=cultura&page=1")]
        [TestCase("cultura-v2", Result = "https://api.twitter.com/1.1/search/tweets.json?rpp=100&result_type=recent&q=cultura-v2&page=1")]
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
            url.Should().Be(string.Format("https://api.twitter.com/1.1/search/tweets.json?rpp=100&result_type=recent&q=cultura&since={0}&page=1", period.Begin.ToString("yyyy-MM-dd")));
        }

        [TestCase(Language.Portuguese, Result = "https://api.twitter.com/1.1/search/tweets.json?rpp=100&result_type=recent&q=cultura&lang=pt&page=1")]
        [TestCase(Language.English, Result = "https://api.twitter.com/1.1/search/tweets.json?rpp=100&result_type=recent&q=cultura&lang=en&page=1")]
        [TestCase(Language.Undefined, Result = "https://api.twitter.com/1.1/search/tweets.json?rpp=100&result_type=recent&q=cultura&page=1")]
        public string WithQuery_IncludeLanguageInQuery(Language language)
        {
            return this.builder.WithQuery(new Query { Term = "cultura", Language = language });
        }

        [TestCase(Country.Brazil, Result = "https://api.twitter.com/1.1/search/tweets.json?rpp=100&result_type=recent&q=cultura&geocode=-14.2350040,-51.9252800,1000km&page=1")]
        [TestCase(Country.Mexico, Result = "https://api.twitter.com/1.1/search/tweets.json?rpp=100&result_type=recent&q=cultura&geocode=23.6345010,-102.5527840,1000km&page=1")]
        [TestCase(Country.Undefined, Result = "https://api.twitter.com/1.1/search/tweets.json?rpp=100&result_type=recent&q=cultura&page=1")]
        public string WithQuery_IncludeCountryInQuery(Country country)
        {
            return this.builder.WithQuery(new Query { Term = "cultura", Country = country });
        }


        [TestCase(1, Result = "https://api.twitter.com/1.1/search/tweets.json?rpp=100&result_type=recent&q=cultura&page=1")]
        [TestCase(2, Result = "https://api.twitter.com/1.1/search/tweets.json?rpp=100&result_type=recent&q=cultura&page=2")]
        public string WithQuery_IncludePageParameter(int pageNumber)
        {
            return this.builder.WithQuery(new Query { Term = "cultura", Page = pageNumber });
        }
    }
}
