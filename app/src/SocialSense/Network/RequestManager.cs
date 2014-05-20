using System;
using System.Net;
using System.IO;
using System.Text;

namespace SocialSense.Network
{
    public class RequestManager
    {
		public void Execute(HttpRequest request, Action<HttpResponse> callback) 
		{
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create (request.Uri.ToString ());
			webRequest.Method = request.Method.ToString ();

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

