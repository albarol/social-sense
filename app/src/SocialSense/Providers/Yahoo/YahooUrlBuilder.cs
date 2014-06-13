using System;
using System.Text;

using SocialSense.Shared;
using SocialSense.UrlBuilders.Parameters;
using SocialSense.UrlBuilders;

namespace SocialSense.Providers.Yahoo
{
    public class YahooUrlBuilder : IUrlBuilder
    {
        private readonly ILocation location;
        private StringBuilder builder;
        
        public YahooUrlBuilder()
        {
            this.location = new YahooLocation();
        }

        public string WithQuery(Query query)
        {
            if (string.IsNullOrEmpty(query.Term))
            {
                throw new ArgumentException("term not be null -or- term not be empty");
            }

			this.builder = new StringBuilder (GetUriByCountry(query.Country));
			this.builder.AppendFormat("&p={0}", query.Term);

            this.AppendCountry(query.Country);
            this.AppendLanguage(query.Language);
            this.AppendPeriod(query.Period);
            this.AppendPage(query.Page);

            return this.builder.ToString();
        }

		private String GetUriByCountry(Country? country)
		{
			if (!country.HasValue || string.IsNullOrEmpty(this.location.GetCountry(country.Value))) {
				return "http://news.search.yahoo.com/search?ei=UTF-8&n=100";
			} else {
				return string.Format("http://{0}.news.search.yahoo.com/search?ei=UTF-8&n=100", this.location.GetCountry(country.Value));
			}
		}

        private void AppendLanguage(Language? language)
        {
            if (language.HasValue && language.Value != Language.Undefined)
            {
                this.builder.AppendFormat("&vl={0}", this.location.GetLanguage(language.Value));
            }
        }

        private void AppendCountry(Country? country)
        {
            if (country.HasValue)
            {
                this.builder.AppendFormat("&vc={0}", this.location.GetCountry(country.Value));
            }
            else
            {
                this.builder.AppendFormat("&vc={0}", this.location.GetCountry(Country.Undefined));
            }
        }

        private void AppendPeriod(Period? period)
        {
			const string PeriodPattern = "&age={0}";

            if (period.HasValue)
            {
                if (period.Value.Equals(Period.Today))
                {
					this.builder.AppendFormat(PeriodPattern, "1d");
                }
                else if (period.Value.Equals(Period.Week))
                {
					this.builder.AppendFormat(PeriodPattern, "1w");
                }
                else if (period.Value.Equals(Period.Month))
                {
                    this.builder.AppendFormat(PeriodPattern, "");
                }
            }
        }

        private void AppendPage(int page)
        {
            this.builder.AppendFormat("&b={0}", (100 * (page - 1)) + 1);
        }
    }
}
