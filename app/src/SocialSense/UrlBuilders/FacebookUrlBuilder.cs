namespace SocialSense.UrlBuilders
{
    using System;
    using System.Text;

    using SocialSense.Shared;
    using SocialSense.UrlBuilders.Parameters;

    public class FacebookUrlBuilder : IUrlBuilder
    {
        private readonly ILocation location;
        private StringBuilder builder;
        private string accessToken;

        public FacebookUrlBuilder()
        {
            this.location = new FacebookLocation();
        }

        public string WithQuery(Query query)
        {
            if (string.IsNullOrEmpty(query.Term))
            {
                throw new ArgumentException("term not be null -or- term not be empty");
            }

            this.builder = new StringBuilder("https://graph.facebook.com/search?type=post&metadata=1&callback=display&limit=50");
            this.builder.AppendFormat("&q={1}", this.accessToken, query.Term);

            this.AppendRegion(query.Language, query.Country);
            this.AppendPeriod(query.Period);
            this.AppendPage(query.Page);

            return this.builder.ToString();
        }

        private void AppendRegion(Language? language, Country? country)
        {
            if ((language.HasValue && language.Value != Language.Undefined) &&
                (country.HasValue && country.Value != Country.Undefined))
            {
                this.builder.AppendFormat("&locate={0}_{1}", this.location.GetLanguage(language.Value), this.location.GetCountry(country.Value));
            }
        }

        private void AppendPeriod(Period? period)
        {
            if (period.HasValue)
            {
                this.builder.AppendFormat("&since={0}", period.Value.Begin.ToString("yyyyMMdd"));
            }
        }

        private void AppendPage(int page)
        {
            this.builder.AppendFormat("&offset={0}", (page - 1) * 100);
        }
    }
}
