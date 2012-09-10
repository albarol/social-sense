namespace SocialSense.Tests.Unit.Urls
{
    using System;

    using NUnit.Framework;

    using SharpTestsEx;

    using SocialSense.Shared;
    using SocialSense.UrlBuilders;

    [TestFixture, Category("UrlBuilder")]
    public class GoogleBlogsUrlBuilderFixture
    {
        private IUrlBuilder builder;
            
        [SetUp]
        public void SetUp()
        {
            this.builder = new GoogleUrlBuilder(GoogleSource.Blogs);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void WithQuery_ThrowsExceptionWhenTermIsNullOrEmpty()
        {
            this.builder.WithQuery(new Query());
        }

        [TestCase("cultura", Result = "http://www.google.com/search?&safe=images&tbm=blg&num=100&q=cultura&tbs=qdr:h&start=0")]
        [TestCase("cultura-v2", Result = "http://www.google.com/search?&safe=images&tbm=blg&num=100&q=cultura-v2&tbs=qdr:h&start=0")]
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
            url.Should().Be.EqualTo(string.Format("http://www.google.com/search?&safe=images&tbm=blg&num=100&q=cultura&tbs=cdr:1,cd_min:{0},cd_max:{1}&start=0", period.Begin.ToString("yyyy-MM-dd"), period.End.ToString("yyyy-MM-dd")));
        }

        [TestCase(Language.Portuguese, Result = "http://www.google.com/search?&safe=images&tbm=blg&num=100&q=cultura&lr=lang_pt&tbs=qdr:h&start=0")]
        [TestCase(Language.English, Result = "http://www.google.com/search?&safe=images&tbm=blg&num=100&q=cultura&lr=lang_en&tbs=qdr:h&start=0")]
        [TestCase(Language.Undefined, Result = "http://www.google.com/search?&safe=images&tbm=blg&num=100&q=cultura&tbs=qdr:h&start=0")]
        public string WithQuery_IncludeLanguageInQuery(Language language)
        {
            return this.builder.WithQuery(new Query { Term = "cultura", Language = language });
        }

        [TestCase(Country.Brazil, Result = "http://www.google.com/search?&safe=images&tbm=blg&num=100&q=cultura&cr=countryBR&tbs=qdr:h&start=0")]
        [TestCase(Country.UnitedStatesOfAmerica, Result = "http://www.google.com/search?&safe=images&tbm=blg&num=100&q=cultura&cr=countryUS&tbs=qdr:h&start=0")]
        [TestCase(Country.Undefined, Result = "http://www.google.com/search?&safe=images&tbm=blg&num=100&q=cultura&tbs=qdr:h&start=0")]
        public string WithQuery_IncludeCountryInQuery(Country country)
        {
            return this.builder.WithQuery(new Query { Term = "cultura", Country = country });
        }


        [TestCase(1, Result = "http://www.google.com/search?&safe=images&tbm=blg&num=100&q=cultura&tbs=qdr:h&start=0")]
        [TestCase(2, Result = "http://www.google.com/search?&safe=images&tbm=blg&num=100&q=cultura&tbs=qdr:h&start=1")]
        public string WithQuery_IncludePageParameter(int pageNumber)
        {
            return this.builder.WithQuery(new Query { Term = "cultura", Page = pageNumber });
        }
    }
}
