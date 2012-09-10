namespace SocialSense.Tests.Integration.Engines
{
    using NUnit.Framework;

    using SharpTestsEx;

    using SocialSense.Engines;
    using SocialSense.Engines.Configurations;
    using SocialSense.Shared;

    [TestFixture, Category("Engines")]
    public class BingEngineFixture
    {
        private Engine engine;

        [SetUp]
        public void SetUp()
        {
            this.engine = new Engine(new BingEngineConfiguration());
        }

        [TestCase("cultura")]
        [TestCase("bing")]
        public void Search_GetResultFromQuery(string term)
        {
            var results = this.engine.Search(new Query { Term = term, MaxResults = 10 });
            results.Count.Should().Be.GreaterThan(0);
        }

        [Test]
        [Ignore("Bing does not support Period")]
        public void Search_GetResultWithPeriod()
        {
            var results = this.engine.Search(new Query { Term = "cultura", Period = Period.Month, MaxResults = 10 });
            results.Count.Should().Be.GreaterThan(0);
        }

        [TestCase("cultura", Language.Spanish)]
        [TestCase("culture", Language.English)]
        public void Search_GetResultWithLanguage(string term, Language language)
        {
            var results = this.engine.Search(new Query { Term = term, Language = language, MaxResults = 10 });
            results.Count.Should().Be.GreaterThan(0);
        }

        [TestCase(Country.Brazil)]
        [TestCase(Country.UnitedStatesOfAmerica)]
        public void Search_GetResultWithCoutry(Country country)
        {
            var results = this.engine.Search(new Query { Term = "cultura", Country = country, MaxResults = 10 });
            results.Count.Should().Be.GreaterThan(0);
        }

        [Test]
        public void Search_NavigateInNextPage()
        {
            var results = this.engine.Search(new Query { Term = "cultura", MaxResults = 20 });
            results.Count.Should().Be.GreaterThanOrEqualTo(20);
        }
    }
}
