using System;
using NUnit.Framework;
using SocialSense.Shared;
using FluentAssertions;
using System.Threading;
using Moq;
using System.Collections.Generic;
using SocialSense.Providers;

namespace SocialSense.Tests.Unit.Providers
{
	[TestFixture, Category("Flows")]
	public class YahooNewsFinderTestCase
	{
		private readonly IFinder finder;

		public YahooNewsFinderTestCase()
		{
			this.finder = Engine.YahooNews ();
		}

		[Test]
		public void Search_VerifyIfGoogleSiteReturnSomeResult()
		{
			var resetEvent = new ManualResetEvent(false);
			this.finder.Search(new Query { Term = "google", MinResults = 20 }, (results) => {
				results.Count.Should().BeGreaterOrEqualTo(20);
				resetEvent.Set ();
			});
			resetEvent.WaitOne ();
		}

		[Test]
		public void Search_SearchSomeResultUsingLanguage()
		{
			var resetEvent = new ManualResetEvent(false);
			var query = new Query { Term = "google", Language = Language.Portuguese, MinResults = 20 };
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
			var query = new Query { Term = "country", Country = Country.RussianFederation, MinResults = 20, Period = Period.Today };
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

