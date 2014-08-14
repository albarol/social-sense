namespace SocialSense.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;

    using HtmlAgilityPack;

    using SocialSense.Shared;

    public class GoogleNewsParser : HtmlParser
    {
        private bool hasNextPage;

        protected override HtmlNode LoadHtmlDocument(string content)
        {
            this.hasNextPage = this.HasNextPage(content);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(content);
            var rootNode = htmlDocument.DocumentNode.SelectSingleNode("//div[@id=\"ires\"]");
            return (rootNode != null && this.ContainsResults(content)) ? rootNode.SelectSingleNode("ol") : null;
        }

        protected override ParserResult ExtractResultsFromHtml(HtmlNode parentNode)
        {
            if (parentNode == null)
            {
                return new ParserResult();
            }

            IList<ResultItem> results = new List<ResultItem>();

            foreach (var node in parentNode.ChildNodes)
            {
                if (node.SelectSingleNode("h3") == null)
                {
                    results.Add(this.GetResultInNode(node));
                }
            }

            return new ParserResult
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
            var builder = node.SelectSingleNode("table/tr/td/h3/a").Attributes["href"].Value;
            builder = builder.Replace("/url?q=", string.Empty);
            builder = builder.Substring(0, builder.IndexOf("&", StringComparison.Ordinal));
            return HttpUtility.UrlDecode(builder);
        }

        protected override string ExtractSnippet(HtmlNode node)
        {
            var htmlNode = node.SelectSingleNode("table/tr/td/div");
            return (htmlNode != null) ? htmlNode.InnerText : string.Empty;
        }

        protected override DateTime ExtractDate(HtmlNode node)
        {
            try
            {
                var itens = node.SelectSingleNode("table/tr/td/span").InnerText.Split('-');
                return DateParser.Parse(itens[itens.Length - 1].Trim());
            }
            catch
            {
                return DateTime.Now;
            }
        }

        protected override string ExtractTitle(HtmlNode node)
        {
            return node.SelectSingleNode("table/tr/td/h3/a").InnerText;
        }

        protected override string ExtractAuthor(HtmlNode node)
        {
            try
            {
                var itens = node.SelectSingleNode("table/tr/td/span").InnerText.Split('-');
                return itens[0].Trim();
            }
            catch
            {
                return string.Empty;
            }
        }

        private bool HasNextPage(string content)
        {
            return content.ToLower().IndexOf("id=\"pnnext\"", StringComparison.Ordinal) > -1;
        }

        private bool ContainsResults(string content)
        {
            const string Pattern = "<div id=\"subform_ctrl\">";
            var regex = new Regex(Pattern);
            return regex.IsMatch(content);
        }
    }
}
