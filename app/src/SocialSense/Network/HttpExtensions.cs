using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace SocialSense.Network
{
	internal class HttpExtensions
    {
		public static string ConvertToGetParameters(IDictionary<string, string> parameters)
		{
			var builder = new StringBuilder("?");
			foreach (var param in parameters)
			{
				builder.AppendFormat("{0}={1}&", param.Key, param.Value);
			}
			builder.Remove(builder.Length - 1, 1);
			return builder.ToString();
		}

		public static byte[] ConvertToPostParameters(IDictionary<string, string> parameters)
		{
			var builder = new StringBuilder();
			foreach (var param in parameters)
			{
				builder.AppendFormat("{0}={1}&", param.Key, HttpUtility.UrlEncode(param.Value.ToString()));
			}
			builder.Remove(builder.Length - 1, 1);
			return Encoding.UTF8.GetBytes(builder.ToString());
		}
    }
}

