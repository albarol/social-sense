using System;
using System.Configuration;

namespace SocialSense
{
    public class GooglePlusElement : ConfigurationElement
    {
        [ConfigurationProperty("token", IsRequired = true)]
        public string Token
        {
            get { return Convert.ToString (this ["token"]); }
            set { this ["token"] = value.ToString (); }
        }
    }
}

