namespace SocialSense.Parsers
{
    using System;

    using HtmlAgilityPack;

    using SocialSense.Shared;

    public abstract class HtmlParser : IParser
    {
        public SearchResult Parse(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException("content not be null -or- content not be empty");
            }
            var parentNode = this.LoadHtmlDocument(content);
            return this.ExtractResultsFromHtml(parentNode);
        }

        protected abstract SearchResult ExtractResultsFromHtml(HtmlNode parentNode);
        protected abstract HtmlNode LoadHtmlDocument(string content);

        protected virtual string ExtractUrl(HtmlNode node)
        {
            return string.Empty;
        }

        protected virtual string ExtractTitle(HtmlNode node)
        {
            return string.Empty;
        }

        protected virtual string ExtractSnippet(HtmlNode node)
        {
            return string.Empty;
        }

        protected virtual DateTime ExtractDate(HtmlNode node)
        {
            return DateTime.Now;
        }

        protected virtual string ExtractAuthor(HtmlNode node)
        {
            return string.Empty;
        }
    }
}
