using System;
using System.Collections.Generic;
using System.Text;

using SocialSense.Shared;
using SocialSense.UrlBuilders;
using SocialSense.UrlBuilders.Parameters;

namespace SocialSense.Providers.GooglePlus
{
    public class GooglePlusUrlBuilder : IUrlBuilder
    {
        private readonly ILocation location;
        private StringBuilder builder;

        public GooglePlusUrlBuilder()
        {
            this.location = new GooglePlusLocation();
        }

        public string WithQuery(Query query)
        {
            if (string.IsNullOrEmpty(query.Term))
            {
                throw new ArgumentException("term not be null -or- term not be empty");
            }

            this.builder = new StringBuilder("https://www.googleapis.com/plus/v1/activities?fields=items(actor%2FdisplayName%2Cobject(attachments(content%2CobjectType)%2Ccontent%2CobjectType)%2Cpublished)%2CnextLink%2Ctitle%2Cupdated&pp=1");
            this.builder.AppendFormat ("&token={0}", CrawlerSection.GetSection().Bing.Token);
            this.builder.AppendFormat("&orderBy=recent&maxResults=20&query={0}", query.Term);
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
            if (parameters.ContainsKey("nextPageToken"))
            {
                this.builder.AppendFormat("&nextPageToken={0}", parameters["nextPageToken"]);    
            }
        }
    }
}
