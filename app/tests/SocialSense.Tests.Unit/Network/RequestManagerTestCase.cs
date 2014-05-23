using System;
using NUnit.Framework;
using SocialSense.Network;
using FluentAssertions;
using System.Threading;

namespace SocialSense.Tests.Unit.Network
{
	[TestFixture, Category("Network")]
	public class RequestManagerTestCase
    {
		private RequestManager requester;

		[SetUp]
		public void SetUp()
		{
			this.requester = new RequestManager();
		}

		[Test]
		public void Get_ShouldGetContent()
		{
			var resetEvent = new ManualResetEvent(false);
			HttpRequest request = new HttpRequest ("http://www.google.com.br");
			this.requester.Execute (request, (response) => {
				response.Content.Should ().NotBeNullOrEmpty ();
				resetEvent.Set ();
			});
			resetEvent.WaitOne ();
		}

		[Test]
		public void Get_ShouldGetContentUsingHeaders()
		{
			var resetEvent = new ManualResetEvent(false);
			HttpRequest request = new HttpRequest ("http://www.google.com.br");
			request.Headers.Add ("token", "test_token");
			this.requester.Execute (request, (response) => {
				response.Content.Should ().NotBeNullOrEmpty ();
				resetEvent.Set ();
			});
			resetEvent.WaitOne ();
		}

		[Test]
		public void Get_ShouldGetContentUsingParameters()
		{
			var resetEvent = new ManualResetEvent(false);
			HttpRequest request = new HttpRequest ("http://www.google.com.br");
			request.Parameters.Add ("token", "test_token");
			this.requester.Execute (request, (response) => {
				response.Content.Should ().NotBeNullOrEmpty ();
				resetEvent.Set ();
			});
			resetEvent.WaitOne ();
		}
		
    }
}
