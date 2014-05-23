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
			this.flow.Search(new Query { Term = "socialsense", MaxResults = 10 }, (results) => {
				results.Count.Should().BeLessOrEqualTo(10);
				resetEvent.Set ();
			});
			resetEvent.WaitOne ();
		}
    }
}

