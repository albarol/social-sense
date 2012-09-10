namespace SocialSense.UrlBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SocialSense.Shared;
    using SocialSense.UrlBuilders.Parameters;

    public class GooglePlusUrlBuilder : IUrlBuilder
    {
        private readonly ILocation location;
        private readonly string apiKey;
        private StringBuilder builder;
        
        public GooglePlusUrlBuilder(string apiKey)
        {
            this.apiKey = apiKey;
            this.location = new GooglePlusLocation();
        }

        public string WithQuery(Query query)
        {
            if (string.IsNullOrEmpty(query.Term))
            {
                throw new ArgumentException("term not be null -or- term not be empty");
            }

            this.builder = new StringBuilder("https://www.googleapis.com/plus/v1/activities?fields=items(actor%2FdisplayName%2Cobject(attachments(content%2CobjectType)%2Ccontent%2CobjectType)%2Cpublished)%2CnextLink%2Ctitle%2Cupdated&pp=1");
            this.builder.AppendFormat("&orderBy=recent&maxResults=20&key={0}&query={1}", this.apiKey, query.Term);
            this.AppendLanguage(query.Language);
            this.AppendPage(query.Parameters);
            return this.builder.ToString();
        }

        private void AppendLanguage(Language? language)
        {
            if (language.HasValue && language.Value != Language.Undefined)
            {
                this.builder.AppendFormat("&language={0}", this.location.GetLanguage(language.Value));
            }
        }

        private void AppendPeriod(Period? period)
        {
            if (period.HasValue)
            {
                var dateDiff = period.Value.End - period.Value.Begin;
                
                this.builder.AppendFormat("&d={0}", Convert.ToInt32(dateDiff.TotalDays));
            }
        }

        private void AppendPage(IDictionary<string, string> parameters)
        {
            if (parameters.ContainsKey("pageToken"))
            {
                this.builder.AppendFormat("&pageToken={0}", parameters["pageToken"]);    
            }
        }
    }
}
