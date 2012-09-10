namespace SocialSense.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using HtmlAgilityPack;

    using SocialSense.Shared;

    public class YahooParser : HtmlParser
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

        protected override SearchResult ExtractResultsFromHtml(HtmlNode parentNode)
        {
            if (parentNode == null)
            {
                return new SearchResult { Items = new List<ResultItem>() };
            }

            var results = new List<ResultItem>();
            foreach (HtmlNode node in parentNode.ChildNodes.Where(c => c.Name == "li"))
            {
                if (this.IsValidNode(node))
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
            }

            return new SearchResult { Items = results, HasNextPage = this.hasNextPage };
        }

        protected override string ExtractUrl(HtmlNode node)
        {
            const int PartWithUrl = 2;

            var dirtyUrl = HttpUtility.HtmlDecode(node.SelectSingleNode("div/div/div/a").Attributes["href"].Value).Split('*');

            if (dirtyUrl.Length > PartWithUrl && dirtyUrl[PartWithUrl] != null)
            {
                return dirtyUrl[PartWithUrl];
            }

            return string.Empty;
        }

        protected override string ExtractTitle(HtmlNode node)
        {
            return node.SelectSingleNode("div/div/div/a").InnerText;
        }

        protected override string ExtractSnippet(HtmlNode node)
        {
            return node.SelectSingleNode("div/div/div/div[1]").InnerText;
        }

        protected override DateTime ExtractDate(HtmlNode node)
        {
            try
            {
                var dateOfResult = node.SelectSingleNode("div/div/div/div[2]").InnerText.Split('-')[1];
                return DateTime.Parse(dateOfResult);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        protected override string ExtractAuthor(HtmlNode node)
        {
            return node.SelectSingleNode("div/div/div/div[2]").InnerText.Split('-')[0].Trim();
        }

        private bool HasNextPage(string content)
        {
            return content.ToLower().IndexOf("id=\"pg\"", StringComparison.Ordinal) > -1;
        }

        private bool IsValidNode(HtmlNode node)
        {
            return node.SelectSingleNode("div/div/div").Attributes["class"] != null &&
                   node.SelectSingleNode("div/div/div").Attributes["class"].Value.Contains("news_text");
        }
    }
}
