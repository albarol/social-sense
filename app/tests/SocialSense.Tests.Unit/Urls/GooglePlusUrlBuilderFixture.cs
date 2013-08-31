namespace SocialSense.Tests.Unit.Urls
{
    using System;

    using NUnit.Framework;

    using SocialSense.Shared;
    using SocialSense.UrlBuilders;

    [TestFixture, Category("UrlBuilder")]
    public class GooglePlusUrlBuilderFixture
    {
        private IUrlBuilder builder;
            
        [SetUp]
        public void SetUp()
        {
            this.builder = new GooglePlusUrlBuilder();
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void WithQuery_ThrowsExceptionWhenTermIsNullOrEmpty()
        {
            this.builder.WithQuery(new Query());
        }

        [TestCase("cultura", Result = "https://www.googleapis.com/plus/v1/activities?fields=items(actor%2FdisplayName%2Cobject(attachments(content%2CobjectType)%2Ccontent%2CobjectType)%2Cpublished)%2CnextLink%2Ctitle%2Cupdated&pp=1&orderBy=recent&maxResults=20&query=cultura")]
        [TestCase("cultura-v2", Result = "https://www.googleapis.com/plus/v1/activities?fields=items(actor%2FdisplayName%2Cobject(attachments(content%2CobjectType)%2Ccontent%2CobjectType)%2Cpublished)%2CnextLink%2Ctitle%2Cupdated&pp=1&orderBy=recent&maxResults=20&query=cultura-v2")]
        public string WithQuery_IncludeTermInQuery(string term)
        {
            return this.builder.WithQuery(new Query { Term = term });
        }

        [Ignore("Google Plus does not support period")]
        public void WithQuery_IncludePeriodInQuery()
        {
            // Arrange:
            var period = Period.Today;
            var query = new Query { Term = "cultura", Period = period };
            
            // Act:
            var url = this.builder.WithQuery(query);

            // Assert:
            
        }

        [TestCase(Language.Portuguese, Result = "https://www.googleapis.com/plus/v1/activities?fields=items(actor%2FdisplayName%2Cobject(attachments(content%2CobjectType)%2Ccontent%2CobjectType)%2Cpublished)%2CnextLink%2Ctitle%2Cupdated&pp=1&orderBy=recent&maxResults=20&query=cultura&language=pt-PT")]
        [TestCase(Language.English, Result = "https://www.googleapis.com/plus/v1/activities?fields=items(actor%2FdisplayName%2Cobject(attachments(content%2CobjectType)%2Ccontent%2CobjectType)%2Cpublished)%2CnextLink%2Ctitle%2Cupdated&pp=1&orderBy=recent&maxResults=20&query=cultura&language=en-US")]
        [TestCase(Language.Undefined, Result = "https://www.googleapis.com/plus/v1/activities?fields=items(actor%2FdisplayName%2Cobject(attachments(content%2CobjectType)%2Ccontent%2CobjectType)%2Cpublished)%2CnextLink%2Ctitle%2Cupdated&pp=1&orderBy=recent&maxResults=20&query=cultura")]
        public string WithQuery_IncludeLanguageInQuery(Language language)
        {
            return this.builder.WithQuery(new Query { Term = "cultura", Language = language });
        }

        [Ignore("Google Plus does not support country")]
        public string WithQuery_IncludeCountryInQuery(Country country)
        {
            return this.builder.WithQuery(new Query { Term = "cultura", Country = country });
        }


        [Ignore]
        [TestCase(1, Result = "https://www.googleapis.com/customsearch/v1?key=ppppppppppppppppp&q=cultura&start=1")]
        [TestCase(2, Result = "https://www.googleapis.com/customsearch/v1?key=ppppppppppppppppp&q=cultura&start=2")]
        public string WithQuery_IncludePageParameter(int pageNumber)
        {
            return this.builder.WithQuery(new Query { Term = "cultura", Page = pageNumber });
        }
    }
}
