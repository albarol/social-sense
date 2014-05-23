using System;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Specialized;

namespace SocialSense.Network
{
    public class RequestManager
    {
		public void Execute(HttpRequest request, Action<HttpResponse> callback) 
		{
			switch (request.Method) {
			case HttpMethod.GET:
				Get (request, callback);
				break;
			case HttpMethod.POST:
				Post (request, callback);
				break;
			}
		}

		private void Get(HttpRequest request, Action<HttpResponse> callback)
		{
			var finalUrl = string.Format ("{0}{1}", request.Uri.ToString (), HttpExtensions.ConvertToGetParameters(request.Parameters)); 
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create (finalUrl);
			webRequest.Method = request.Method.ToString ();
			foreach (var item in request.Headers) 
			{
				webRequest.Headers.Set (item.Key, item.Value);
			}

			HttpReply reply = new HttpReply (webRequest, callback);
			webRequest.BeginGetResponse (new AsyncCallback (ProcessResponse), reply);
		}

		private void Post(HttpRequest request, Action<HttpResponse> callback)
		{
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create (request.Uri.ToString ());
			webRequest.Method = request.Method.ToString ();
			webRequest.ContentType = "application/x-www-form-urlencoded";

			foreach (var item in request.Headers) 
			{
				webRequest.Headers.Set (item.Key, item.Value);
			}

			if (request.Parameters.Count > 0)
			{
				byte[] data = HttpExtensions.ConvertToPostParameters(request.Parameters);
				webRequest.ContentLength = data.Length;

				using (Stream stream = webRequest.GetRequestStream())
				{
					stream.Write(data, 0, data.Length);
				}
			}

			HttpReply reply = new HttpReply (webRequest, callback);
			webRequest.BeginGetResponse (new AsyncCallback (ProcessResponse), reply);
		}

		private void ProcessResponse(IAsyncResult responseResult)
		{
			HttpReply reply = (HttpReply)responseResult.AsyncState;
			HttpWebRequest webRequest = reply.Request;
			HttpWebResponse webResponse = (HttpWebResponse)webRequest.EndGetResponse (responseResult);
			StreamReader reader = string.IsNullOrEmpty(webResponse.CharacterSet) ?
			                      new StreamReader(webResponse.GetResponseStream()) :
			                      new StreamReader(webResponse.GetResponseStream(), Encoding.GetEncoding(webResponse.CharacterSet));
			reply.Callback (new HttpResponse { Content = reader.ReadToEnd (), StatusCode = webResponse.StatusCode });
		}
    }
}

