namespace SocialSense.Tests.Unit.Urls
{
    using System;

    using NUnit.Framework;

    using SharpTestsEx;

    using SocialSense.Shared;
    using SocialSense.UrlBuilders;

    [TestFixture, Category("UrlBuilder")]
    public class BingUrlBuilderFixture
    {
        private IUrlBuilder builder;

        [SetUp]
        public void SetUp()
        {
            this.builder = new BingUrlBuilder();
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void WithQuery_ThrowsExceptionWhenTermIsNullOrEmpty()
        {
            this.builder.WithQuery(new Query());
        }

        [TestCase("cultura", Result = "http://www.bing.com/search?q=cultura&first=1")]
        [TestCase("cultura-v2", Result = "http://www.bing.com/search?q=cultura-v2&first=1")]
        public string WithQuery_IncludeTermInQuery(string term)
        {
            return this.builder.WithQuery(new Query { Term = term });
        }

        [Ignore("Bing does not support period")]
        [Test]
        public void WithQuery_IncludePeriodInQuery()
        {
            // Arrange:
            var period = Period.Today;
            var query = new Query { Term = "cultura", Period = period };

            // Act:
            var url = this.builder.WithQuery(query);

            // Assert:
            url.Should().Be.EqualTo("http://www.bing.com/search?q=cultura");
        }

        [TestCase(Language.Portuguese, Result = "http://www.bing.com/search?q=cultura +language:pt_br&first=1")]
        [TestCase(Language.English, Result = "http://www.bing.com/search?q=cultura +language:en&first=1")]
        [TestCase(Language.Undefined, Result = "http://www.bing.com/search?q=cultura&first=1")]
        public string WithQuery_IncludeLanguageInQuery(Language language)
        {
            return this.builder.WithQuery(new Query { Term = "cultura", Language = language });
        }

        [TestCase(Country.Brazil, Result = "http://www.bing.com/search?q=cultura +loc:BR&first=1")]
        [TestCase(Country.Mexico, Result = "http://www.bing.com/search?q=cultura +loc:MX&first=1")]
        [TestCase(Country.Undefined, Result = "http://www.bing.com/search?q=cultura&first=1")]
        public string WithQuery_IncludeCountryInQuery(Country country)
        {
            return this.builder.WithQuery(new Query { Term = "cultura", Country = country });
        }


        [TestCase(1, Result = "http://www.bing.com/search?q=cultura&first=1")]
        [TestCase(2, Result = "http://www.bing.com/search?q=cultura&first=11")]
        public string WithQuery_IncludePageParameter(int pageNumber)
        {
            return this.builder.WithQuery(new Query { Term = "cultura", Page = pageNumber });
        }
    }
}
