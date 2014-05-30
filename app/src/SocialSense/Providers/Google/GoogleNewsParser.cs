using System;
using SocialSense.Parsers;
using HtmlAgilityPack;
using SocialSense.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialSense.Providers.Google
{
    internal class GoogleNewsParser : HtmlParser
	{
		private bool hasNextPage;

		protected override HtmlNode LoadHtmlDocument(string content)
		{
			this.hasNextPage = this.HasNextPage(content);

			var htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(content);
			var rootNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@id=\"ires\"]");
			return (rootNode != null) ? rootNode.SelectSingleNode("ol") : null;
		}

		protected override SearchResult ExtractResultsFromHtml(HtmlNode parentNode)
		{
			if (parentNode == null)
			{
				return new SearchResult();
			}

			IList<ResultItem> results = new List<ResultItem>();

            foreach (var node in parentNode.ChildNodes)
			{
                var contentNode = node.SelectSingleNode ("table/tr[1]/td");
                results.Add(this.GetResultInNode(contentNode));
			}

			return new SearchResult
			{
				Items = results.Where(r => !string.IsNullOrEmpty(r.Snippet)).ToList(),
				HasNextPage = this.hasNextPage
			};
		}

		protected ResultItem GetResultInNode(HtmlNode node)
		{
            return new ResultItem
			{
				Url = this.ExtractUrl(node),
				Date = this.ExtractDate(node),
				Snippet = this.ExtractSnippet(node),
				Title = this.ExtractTitle(node),
				Author = this.ExtractAuthor(node)
			};
		}

		protected override string ExtractUrl(HtmlNode node)
		{
            var builder = node.SelectSingleNode("h3/a").Attributes["href"].Value;
			builder = builder.Replace("/url?q=", string.Empty);
            builder = builder.Substring(0, builder.IndexOf("&", StringComparison.Ordinal));
            return HttpUtility.UrlDecode(builder);
		}

		protected override string ExtractSnippet(HtmlNode node)
		{
            var htmlNode = node.SelectSingleNode("div[2]");
			return (htmlNode != null) ? htmlNode.InnerText : string.Empty;
		}

		protected override DateTime ExtractDate(HtmlNode node)
		{
			try
			{
                var dateText = node.SelectSingleNode("div[1]").InnerText.Split('-')[1].Trim();
                return DateParser.Parse(dateText);
			}
			catch
			{
				return DateTime.Now;
			}
		}

		protected override string ExtractTitle(HtmlNode node)
		{
            return node.SelectSingleNode("h3/a").InnerText;
		}

		protected override string ExtractAuthor(HtmlNode node)
		{
            var builder = node.SelectSingleNode("h3/a").Attributes["href"].Value;
            Uri uri = new Uri (builder.Replace("/url?q=", string.Empty));
            return uri.Host;
		}

		private bool HasNextPage(string content)
		{
			return content.ToLower().IndexOf("id=\"nav\"", StringComparison.Ordinal) > -1;
		}
	}
}

