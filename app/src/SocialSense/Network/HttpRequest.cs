using System;
using System.Collections.Generic;

namespace SocialSense.Network
{
    public class HttpRequest
    {
		private readonly IDictionary<string, string> headers = new Dictionary<string, string>();
		private readonly IDictionary<string, string> parameters = new Dictionary<string, string>();

		public HttpRequest(string uriString)
		{
			this.Uri = new Uri (uriString);
		}

		public HttpRequest(Uri uri, HttpMethod method)
		{
			this.Uri = uri;
			this.Method = method;
		}

		public Uri Uri { get; set; }
		public HttpMethod Method { get;set; }
		public IDictionary<string, string> Headers { get { return headers; } }
		public IDictionary<string, string> Parameters { get { return parameters; } }
    }
}

