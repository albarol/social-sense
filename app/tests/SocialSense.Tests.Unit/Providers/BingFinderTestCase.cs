using System;
using NUnit.Framework;
using SocialSense.Providers;
using System.Threading;
using SocialSense.Shared;
using FluentAssertions;

namespace SocialSense.Tests.Unit.Providers
{
    [TestFixture, Category("Finder")]
    public class BingFinderTestCase
    {
        private readonly IFinder finder;

        public BingFinderTestCase()
        {
            this.finder = Engine.Bing ();
        }

        [Test]
        public void Search_VerifyIfBingApiReturnSomeResult()
        {
            var resetEvent = new ManualResetEvent(false);
            this.finder.Search(new Query { Term = "bing", MinResults = 20 }, (results) => {
                results.Count.Should().BeGreaterOrEqualTo(20);
                resetEvent.Set ();
            });
            resetEvent.WaitOne ();
        }

        [Test]
        public void Search_SearchSomeResultUsingLanguage()
        {
            var resetEvent = new ManualResetEvent(false);
            var query = new Query { Term = "bing", Language = Language.Portuguese, MinResults = 20 };
            this.finder.Search(query, (results) => {
                results.Count.Should().BeGreaterOrEqualTo(20);
                resetEvent.Set ();
            });
            resetEvent.WaitOne ();
        }

        [Test]
        public void Search_SearchSomeResultUsingCountry()
        {
            var resetEvent = new ManualResetEvent(false);
            var query = new Query { Term = "country", Country = Country.RussianFederation, MinResults = 20 };
            this.finder.Search(query, (results) => {
                results.Count.Should().BeGreaterOrEqualTo(20);
                resetEvent.Set ();
            });
            resetEvent.WaitOne ();
        }

        [Test]
        public void Search_SearchSomeResultUsingPeriod()
        {
            var resetEvent = new ManualResetEvent(false);
            var query = new Query { Term = "history", Period = Period.Week, MinResults = 200 };
            this.finder.Search(query, (results) => {
                results.Count.Should().BeGreaterOrEqualTo(20);
                resetEvent.Set ();
            });
            resetEvent.WaitOne ();
        }
    }
}

