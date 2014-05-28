using System;
using NUnit.Framework;
using SocialSense.Shared;
using FluentAssertions;
using System.Threading;

namespace SocialSense.Tests.Unit.Providers
{
	[TestFixture, Category("Flows")]
	public class GoogleSiteFlowTestCase
    {
		private readonly IBasicFlow flow;

		public GoogleSiteFlowTestCase()
		{
			this.flow = Engine.GoogleSite ();
		}

		[Test]
		public void Search_VerifyIfGoogleSiteReturnSomeResult()
		{
			var resetEvent = new ManualResetEvent(false);
            this.flow.Search(new Query { Term = "google", MinResults = 20 }, (results) => {
                results.Count.Should().BeGreaterOrEqualTo(20);
				resetEvent.Set ();
			});
			resetEvent.WaitOne ();
		}
    }
}

