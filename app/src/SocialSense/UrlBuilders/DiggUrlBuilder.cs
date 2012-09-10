namespace SocialSense.UrlBuilders
{
    using System;
    using System.Text;

    using SocialSense.Shared;

    public class DiggUrlBuilder : IUrlBuilder
    {
        private StringBuilder builder;

        public string WithQuery(Query query)
        {
            if (string.IsNullOrEmpty(query.Term))
            {
                throw new ArgumentException("term not be null -or- term not be empty");
            }

            this.builder = new StringBuilder("http://services.digg.com/2.0/search.search?count=100&media=news");
            this.builder.AppendFormat("&query={0}", query.Term);
            this.AppendPeriod(query.Period);
            this.AppendPage(query.Page);

            return this.builder.ToString();
        }

        private void AppendPeriod(Period? period)
        {
            if (period.HasValue)
            {
                this.builder.AppendFormat("&min_date={0}&max_date={1}", DateParser.ToUnixTimestamp(period.Value.Begin), DateParser.ToUnixTimestamp(period.Value.End));
            }
        }

        private void AppendPage(int page)
        {
            this.builder.AppendFormat("&offset={0}", page);
        }
    }
}
