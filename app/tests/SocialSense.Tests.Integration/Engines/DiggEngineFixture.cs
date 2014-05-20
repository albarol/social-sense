﻿namespace SocialSense.Tests.Integration.Engines
{
    using NUnit.Framework;

	using FluentAssertions;

    using SocialSense.Engines;
    using SocialSense.Extensions;
    using SocialSense.Shared;

    [TestFixture, Category("Engines")]
    public class DiggEngineFixture
    {
        private Engine engine;

        [SetUp]
        public void SetUp()
        {
            this.engine = EngineFactory.Digg();
        }

        [TestCase("cultura")]
        [TestCase("digg")]
        public void Search_GetResultFromQuery(string term)
        {
            var results = this.engine.Search(new Query { Term = term, MaxResults = 10 });
            results.Count.Should().BeGreaterThan(0);
        }

        [Test]
        public void Search_GetResultWithPeriod()
        {
            var results = this.engine.Search(new Query { Term = "cultura", Period = Period.Month, MaxResults = 10 });
            results.Count.Should().BeGreaterThan(0);
        }

        [Ignore("Digg does not support language parameter")]
        [Test]
        public void Search_GetResultWithLanguage()
        {
            var results = this.engine.Search(new Query { Term = "cultura", Language = Language.English, MaxResults = 10 });
            results.Count.Should().BeGreaterThan(0);
        }

        [Ignore("Digg does not support country parameter")]
        [Test]
        public void Search_GetResultWithCoutry()
        {
            var results = this.engine.Search(new Query { Term = "cultura", Country = Country.Brazil, MaxResults = 10 });
            results.Count.Should().BeGreaterThan(0);
        }

        [Test]
        public void Search_NavigateInNextPage()
        {
            var results = this.engine.Search(new Query { Term = "cultura", MaxResults = 20 });
            results.Count.Should().BeGreaterOrEqualTo(20);
        }
    }
}
