namespace SocialSense.UrlBuilders
{
    using System;
    using System.Text;
    using System.Web;

    using SocialSense.Shared;
    using SocialSense.UrlBuilders.Parameters;

    public class TwitterUrlBuilder : IUrlBuilder
    {
        private readonly ILocation location;
        private StringBuilder builder;
        
        public TwitterUrlBuilder()
        {
            this.location = new TwitterLocation();
        }

        public string WithQuery(Query query)
        {
            if (string.IsNullOrEmpty(query.Term))
            {
                throw new ArgumentException("term not be null -or- term not be empty");
            }

            this.builder = new StringBuilder("http://search.twitter.com/search.json?rpp=100&result_type=recent");
            this.builder.AppendFormat("&q={0}", HttpUtility.UrlEncode(query.Term));
            
            this.AppendLanguage(query.Language);
            this.AppendCountry(query.Country);
            this.AppendPeriod(query.Period);
            this.AppendPage(query.Page);

            return this.builder.ToString();
        }

        private void AppendLanguage(Language? language)
        {
            if (language.HasValue && language.Value != Language.Undefined)
            {
                this.builder.AppendFormat("&lang={0}", this.location.GetLanguage(language.Value));
            }
        }

        private void AppendCountry(Country? country)
        {
            if (country.HasValue && country.Value != Country.Undefined)
            {
                this.builder.AppendFormat("&geocode={0}", this.location.GetCountry(country.Value));
            }
        }

        private void AppendPeriod(Period? period)
        {
            if (period.HasValue)
            {
                this.builder.AppendFormat("&since={0}", period.Value.Begin.ToString("yyyy-MM-dd"));
            }
        }

        private void AppendPage(int page)
        {
            this.builder.AppendFormat("&page={0}", page);
        }
    }
}
