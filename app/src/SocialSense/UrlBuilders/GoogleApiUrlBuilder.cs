namespace SocialSense.UrlBuilders
{
    using System;
    using System.Text;

    using SocialSense.Shared;
    using SocialSense.UrlBuilders.Parameters;

    public class GoogleApiUrlBuilder : IUrlBuilder
    {
        private readonly ILocation location;
        private readonly string apiKey;
        private StringBuilder builder;
        
        public GoogleApiUrlBuilder(string apiKey)
        {
            this.apiKey = apiKey;
            this.location = new GoogleLocation();
        }

        public string WithQuery(Query query)
        {
            if (string.IsNullOrEmpty(query.Term))
            {
                throw new ArgumentException("term not be null -or- term not be empty");
            }

            this.builder = new StringBuilder("https://www.googleapis.com/customsearch/v1?");
            this.builder.AppendFormat("key={0}&q={1}", this.apiKey, query.Term);
            
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
                this.builder.AppendFormat("&hl={0}", this.location.GetLanguage(language.Value).Replace("lang_", string.Empty));
            }
        }

        private void AppendCountry(Country? country)
        {
            if (country.HasValue && country.Value != Country.Undefined)
            {
                this.builder.AppendFormat("&cr={0}", this.location.GetCountry(country.Value));
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

        private void AppendPage(int page)
        {
            this.builder.AppendFormat("&start={0}", page);
        }
    }
}
