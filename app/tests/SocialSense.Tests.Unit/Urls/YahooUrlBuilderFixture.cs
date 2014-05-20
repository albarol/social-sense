namespace SocialSense.Tests.Unit.Urls
{
    using System;

    using NUnit.Framework;

	using FluentAssertions;

    using SocialSense.Shared;
    using SocialSense.UrlBuilders;

    [TestFixture, Category("UrlBuilder")]
    public class YahooUrlBuilderFixture
    {
        private IUrlBuilder builder;

        [SetUp]
        public void SetUp()
        {
            this.builder = new YahooUrlBuilder();
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void WithQuery_ThrowsExceptionWhenTermIsNullOrEmpty()
        {
            this.builder.WithQuery(new Query());
        }

        [TestCase("cultura", Result = "http://news.search.yahoo.com/search?ei=UTF-8&n=100&p=cultura&vc=&b=1")]
        [TestCase("cultura-v2", Result = "http://news.search.yahoo.com/search?ei=UTF-8&n=100&p=cultura-v2&vc=&b=1")]
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
            url.Should().Be("http://news.search.yahoo.com/search?ei=UTF-8&n=100&p=cultura&vc=&btf=d&b=1");
        }

        [TestCase(Language.Portuguese, Result = "http://news.search.yahoo.com/search?ei=UTF-8&n=100&p=cultura&vc=&vl=lang_pt&b=1")]
        [TestCase(Language.English, Result = "http://news.search.yahoo.com/search?ei=UTF-8&n=100&p=cultura&vc=&vl=lang_en&b=1")]
        [TestCase(Language.Undefined, Result = "http://news.search.yahoo.com/search?ei=UTF-8&n=100&p=cultura&vc=&b=1")]
        public string WithQuery_IncludeLanguageInQuery(Language language)
        {
            return this.builder.WithQuery(new Query { Term = "cultura", Language = language });
        }

        [TestCase(Country.Brazil, Result = "http://news.search.yahoo.com/search?ei=UTF-8&n=100&p=cultura&vc=br&b=1")]
        [TestCase(Country.Mexico, Result = "http://news.search.yahoo.com/search?ei=UTF-8&n=100&p=cultura&vc=mx&b=1")]
        [TestCase(Country.Undefined, Result = "http://news.search.yahoo.com/search?ei=UTF-8&n=100&p=cultura&vc=&b=1")]
        public string WithQuery_IncludeCountryInQuery(Country country)
        {
            return this.builder.WithQuery(new Query { Term = "cultura", Country = country });
        }


        [TestCase(1, Result = "http://news.search.yahoo.com/search?ei=UTF-8&n=100&p=cultura&vc=&b=1")]
        [TestCase(2, Result = "http://news.search.yahoo.com/search?ei=UTF-8&n=100&p=cultura&vc=&b=101")]
        public string WithQuery_IncludePageParameter(int pageNumber)
        {
            return this.builder.WithQuery(new Query { Term = "cultura", Page = pageNumber });
        }
    }
}
