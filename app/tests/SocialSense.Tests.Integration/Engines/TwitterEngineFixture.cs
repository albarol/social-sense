namespace SocialSense.Tests.Integration.Engines
{
    using NUnit.Framework;

    using SharpTestsEx;

    using SocialSense.Engines;
    using SocialSense.Extensions;
    using SocialSense.Shared;

    [TestFixture, Category("Engines")]
    public class TwitterEngineFixture
    {
        private Engine engine;

        [SetUp]
        public void SetUp()
        {
            this.engine = EngineFactory.Twitter();
        }

        [TestCase("cultura")]
        [TestCase("twitter")]
        public void Search_GetResultFromQuery(string term)
        {
            var results = this.engine.Search(new Query { Term = term, MaxResults = 100 });
            results.Count.Should().Be.GreaterThan(0);
        }

        [Test]
        public void Search_GetResultWithPeriod()
        {
            var results = this.engine.Search(new Query { Term = "cultura", Period = Period.Month, MaxResults = 100 });
            results.Count.Should().Be.GreaterThan(0);
        }

        [TestCase(Language.Portuguese)]
        [TestCase(Language.Spanish)]
        public void Search_GetResultWithLanguage(Language language)
        {
            var results = this.engine.Search(new Query { Term = "cultura", Language = language, MaxResults = 100 });
            results.Count.Should().Be.GreaterThan(0);
        }

        [TestCase(Country.Brazil)]
        [TestCase(Country.Spain)]
        public void Search_GetResultWithCoutry(Country country)
        {
            var results = this.engine.Search(new Query { Term = "cultura", Country = country, MaxResults = 100 });
            results.Count.Should().Be.GreaterThan(0);
        }

        [Test]
        public void Search_NavigateInNextPage()
        {
            var results = this.engine.Search(new Query { Term = "cultura", MaxResults = 400 });
            results.Count.Should().Be.GreaterThan(103);
        }
    }
}
