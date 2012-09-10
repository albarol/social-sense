namespace SocialSense.Spiders.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    public class CookieBehavior : Behavior
    {
        private readonly KeyValuePair<string, string> cookie;

        public CookieBehavior(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key not be null -or- empty");
            }

            this.cookie = new KeyValuePair<string, string>(key, value);
        }

        public override void Execute()
        {
            this.Spider.HttpWebRequest.Headers.Add("Cookie", new Cookie(this.cookie.Key, this.cookie.Value).ToString());
        }
    }
}
