using System;
using System.Text;

using SocialSense.Shared;

namespace SocialSense.Providers.Bing
{
    public class BingUrlBuilder
    {
        private readonly ILocation location;
        private StringBuilder builder;

        public BingUrlBuilder()
        {
            this.location = new BingLocation();
        }

        public string WithQuery(Query query)
        {
            if (string.IsNullOrEmpty(query.Term))
            {
                throw new ArgumentException("term not be null -or- term not be empty");
            }

            this.builder = new StringBuilder("http://www.bing.com/search?");
            this.builder.AppendFormat("q={0}", query.Term);

            this.AppendLanguage(query.Language);
            this.AppendCountry(query.Country);
            this.AppendPage(query.Page);

            return this.builder.ToString();
        }

        private void AppendLanguage(Language? language)
        {
            if (language.HasValue && language.Value != Language.Undefined)
            {
                this.builder.AppendFormat(" +language:{0}", this.location.GetLanguage(language.Value));
            }
        }

        private void AppendCountry(Country? country)
        {
            if (country.HasValue && country.Value != Country.Undefined)
            {
                this.builder.AppendFormat(" +loc:{0}", this.location.GetCountry(country.Value));
            }
        }

        private void AppendPage(int page)
        {
            this.builder.AppendFormat("&first={0}", ((page * 10) - 10) + 1);
        }
    }
}
