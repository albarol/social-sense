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

            this.builder = new StringBuilder("https://api.datamarket.azure.com/Bing/Search?format=json");
            this.builder.AppendFormat("&sources=web&query={0}", query.Term);

            this.AppendCountry(query.Country);
            this.AppendPage(query.Page);

            return this.builder.ToString();
        }

        private void AppendCountry(Country? country)
        {
            if (country.HasValue && country.Value != Country.Undefined)
            {
                this.builder.AppendFormat("{0}", this.location.GetCountry(country.Value));
            }
        }

        private void AppendPage(int page)
        {
            //this.builder.AppendFormat("&skip={0}", (page * 50) - 50);
        }
    }
}
