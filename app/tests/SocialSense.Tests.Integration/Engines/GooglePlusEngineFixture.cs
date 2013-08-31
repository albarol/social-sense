using SocialSense.Authorization;

namespace SocialSense.Tests.Integration.Engines
{
    using NUnit.Framework;

    using SharpTestsEx;

    using SocialSense.Engines;
    using SocialSense.Extensions;
    using SocialSense.Shared;

    [TestFixture, Category("Engines")]
    public class GooglePlusEngineFixture
    {
        private Engine engine;

        [SetUp]
        public void SetUp()
        {
            this.engine = EngineFactory.GooglePlus(new GooglePlusAuthorization { ApiKey = "AIzaSyDazHELlbnIFLkH_3p9L5hPo70tJVk-vO4" });
        }

        [TestCase("cultura")]
        [TestCase("google sites")]
        public void Search_GetResultFromQuery(string term)
        {
            var results = this.engine.Search(new Query { Term = term, MaxResults = 10 });
            results.Count.Should().Be.GreaterThan(0);
        }

        [Ignore("Google Plus does not support country")]
        public void Search_GetResultWithPeriod()
        {
            var results = this.engine.Search(new Query { Term = "cultura", Period = Period.Month, MaxResults = 10 });
            results.Count.Should().Be.GreaterThan(0);
        }

        [TestCase(Language.Portuguese)]
        [TestCase(Language.BrazilianPortuguese)]
        public void Search_GetResultWithLanguage(Language language)
        {
            var results = this.engine.Search(new Query { Term = "cultura", Language = language, MaxResults = 10 });
            results.Count.Should().Be.GreaterThan(0);
        }

        [Ignore("Google Plus does not support country")]
        public void Search_GetResultWithCoutry(Country country)
        {
            var results = this.engine.Search(new Query { Term = "cultura", Country = country, MaxResults = 10 });
            results.Count.Should().Be.GreaterThan(0);
        }

        [Test]
        [Ignore]
        public void Search_NavigateInNextPage()
        {
            var results = this.engine.Search(new Query { Term = "google", MaxResults = 20 });
            results.Count.Should().Be.GreaterThan(10);
        }
    }
}
