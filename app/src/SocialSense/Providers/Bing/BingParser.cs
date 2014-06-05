using System;
using System.Collections.Generic;
using System.Linq;

using HtmlAgilityPack;

using SocialSense.Shared;
using SocialSense.Parsers;

namespace SocialSense.Providers.Bing
{
    public class BingParser : HtmlParser
    {
        private bool hasNextPage;

        protected override HtmlNode LoadHtmlDocument(string content)
        {
            this.hasNextPage = this.HasNextPage(content);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(content);
            var rootNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@id=\"results\"]");
            return (rootNode != null) ? rootNode.SelectSingleNode("ul") : null;
        }

        protected override SearchResult ExtractResultsFromHtml(HtmlNode parentNode)
        {
            if (parentNode == null)
            {
                return new SearchResult();
            }

            var results = new List<ResultItem>();

            foreach (var node in parentNode.ChildNodes.Where(l => l.FirstChild != null && l.FirstChild.Name == "div"))
            {
                try
                {
                    var result = new ResultItem
                    {
                        Url = this.ExtractUrl(node),
                        Date = this.ExtractDate(node),
                        Snippet = this.ExtractSnippet(node),
                        Title = this.ExtractTitle(node),
                        Author = this.ExtractAuthor(node)
                    };
                    results.Add(result);
                }
                catch (Exception e)
                {
                    throw new ParserException(e.Message) { Source = e.Source };
                }
            }
            return new SearchResult { Items = results, HasNextPage = this.hasNextPage };
        }

        protected override string ExtractUrl(HtmlNode node)
        {
            var htmlNode = node.SelectSingleNode("div[1]/div[1]/div[1]/h3/a");
            return (htmlNode != null) ? htmlNode.Attributes["href"].Value : string.Empty;
        }

        protected override string ExtractTitle(HtmlNode node)
        {
            var htmlNode = node.SelectSingleNode("div[1]/div[1]/div[1]/h3");
            return (htmlNode != null) ? htmlNode.InnerText : string.Empty;
        }

        protected override string ExtractSnippet(HtmlNode node)
        {
            var htmlNode = node.SelectSingleNode("div[1]/div[1]/p");
            return (htmlNode != null) ? htmlNode.InnerText : string.Empty;
        }

        protected override DateTime ExtractDate(HtmlNode node)
        {
            return DateTime.Now;
        }

        protected override string ExtractAuthor(HtmlNode node)
        {
            return this.ExtractUrl(node);
        }

        private bool HasNextPage(string content)
        {
            return content.Contains("class=\"sb_pag\"");
        }
    }
}
