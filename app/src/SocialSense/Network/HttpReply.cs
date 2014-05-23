using System;
using System.Net;

namespace SocialSense.Network
{
	public class HttpReply
    {
		public HttpWebRequest Request { get; private set; }
		public Action<HttpResponse> Callback { get; private set; }

		public HttpReply(HttpWebRequest request, Action<HttpResponse> callback)
		{
			this.Request = request;
			this.Callback = callback;
		}
    }
}

