using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using HtmlAgilityPack;

using SocialSense.Shared;
using SocialSense.Parsers;

namespace SocialSense.Providers.Yahoo
{
	public class YahooNewsParser : HtmlParser
    {
        private bool hasNextPage;

        protected override HtmlNode LoadHtmlDocument(string content)
        {
            this.hasNextPage = this.HasNextPage(content);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(content);
            var rootNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@id=\"web\"]");
            return (rootNode != null) ? rootNode.SelectSingleNode("ol") : null;
        }

        protected override ParserResult ExtractResultsFromHtml(HtmlNode parentNode)
        {
            if (parentNode == null)
            {
                return new ParserResult { Items = new List<ResultItem>() };
            }

            var results = new List<ResultItem>();
            foreach (HtmlNode node in parentNode.ChildNodes.Where(c => c.Name == "li"))
            {
                if (this.IsValidNode(node))
                {
					results.Add(new ResultItem
					{
						Url = this.ExtractUrl(node),
						Date = this.ExtractDate(node),
						Snippet = this.ExtractSnippet(node),
						Title = this.ExtractTitle(node),
						Author = this.ExtractAuthor(node)
					});
                }
            }

            return new ParserResult { Items = results, HasNextPage = this.hasNextPage };
        }

        protected override string ExtractUrl(HtmlNode node)
        {
			if (node.SelectSingleNode("div/div[1]/h3/a") == null)
            {
				return HttpUtility.HtmlDecode (node.SelectSingleNode ("div/div[1]/div/h3/a").Attributes ["href"].Value);
            }
			return HttpUtility.HtmlDecode (node.SelectSingleNode ("div/div[1]/h3/a").Attributes ["href"].Value);
        }

        protected override string ExtractTitle(HtmlNode node)
        {
			if (node.SelectSingleNode("div/div[1]/h3/a") == null)
            {
				return node.SelectSingleNode("div/div[1]/div/h3/a").InnerText;
            }

			return node.SelectSingleNode("div/div[1]/h3/a").InnerText;
        }

        protected override string ExtractSnippet(HtmlNode node)
        {
			if (node.SelectSingleNode("div/div[2]") == null)
            {
				return node.SelectSingleNode("div/div/div").InnerText;
            }

			return node.SelectSingleNode("div/div[2]").InnerText;
        }

        protected override DateTime ExtractDate(HtmlNode node)
        {
            try
            {
				if (node.SelectSingleNode("div/span[2]") == null) 
				{
					return DateTime.Parse(node.SelectSingleNode("div/div/div/span[2]").InnerText);
				}
				else
				{
					return DateTime.Parse(node.SelectSingleNode("div/span[2]").InnerText);
				}
            }
            catch
            {
                return DateTime.Now;
            }
        }

        protected override string ExtractAuthor(HtmlNode node)
        {
			if (node.SelectSingleNode("div/span[1]") == null)
            {
				return node.SelectSingleNode ("div/div/div/span[1]").InnerText;
            }

			return node.SelectSingleNode ("div/span[1]").InnerText;
        }

        private bool HasNextPage(string content)
        {
            return content.ToLower().IndexOf("id=\"pg\"", StringComparison.Ordinal) > -1;
        }

        private bool IsValidNode(HtmlNode node)
        {
            return node.SelectSingleNode("div") != null &&
                   node.SelectSingleNode("div").Attributes["class"] != null &&
                   node.SelectSingleNode("div").Attributes["class"].Value.Contains("res");
        }
    }
}
