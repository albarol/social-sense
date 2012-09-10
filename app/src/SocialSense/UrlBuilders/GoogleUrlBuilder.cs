namespace SocialSense.UrlBuilders
{
    using System;
    using System.Text;

    using SocialSense.Shared;
    using SocialSense.UrlBuilders.Parameters;

    public class GoogleUrlBuilder : IUrlBuilder
    {
        private readonly ILocation location;
        private readonly GoogleSource source;

        private StringBuilder builder;

        public GoogleUrlBuilder(GoogleSource source)
        {
            this.location = new GoogleLocation();
            this.source = source;
        }

        private string TabSource
        {
            get
            {
                switch (this.source)
                {
                    case GoogleSource.Blogs:
                        return "&tbm=blg";
                    case GoogleSource.News:
                        return "&tbm=nws";
                    default:
                        return string.Empty;
                }
            }
        }
        
        public string WithQuery(Query query)
        {
            if (string.IsNullOrEmpty(query.Term))
            {
                throw new ArgumentException("term not be null -or- term not be empty");
            }

            this.builder = new StringBuilder("http://www.google.com/search?&safe=images");
            this.builder.AppendFormat("{0}&num=100&q={1}", this.TabSource, query.Term);
            
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
                this.builder.AppendFormat("&lr={0}", this.location.GetLanguage(language.Value));
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
                this.builder.AppendFormat("&tbs=cdr:1,cd_min:{0},cd_max:{1}", period.Value.Begin.ToString("yyyy-MM-dd"), period.Value.End.ToString("yyyy-MM-dd"));
            }
            else
            {
                this.builder.AppendFormat("&tbs=qdr:h");
            }
        }

        private void AppendPage(int page)
        {
            this.builder.AppendFormat("&start={0}", (page - 1));
        }
    }
}