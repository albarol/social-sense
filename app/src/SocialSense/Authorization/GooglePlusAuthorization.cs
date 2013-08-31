using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialSense.Authorization
{
    public class GooglePlusAuthorization : IAuthorization
    {
        public AuthorizationVia Via { get { return AuthorizationVia.Url; } }
        public string ApiKey { get; set; }
        
        public string Generate()
        {
            return string.Format("key={0}", ApiKey);
        }
    }
}
