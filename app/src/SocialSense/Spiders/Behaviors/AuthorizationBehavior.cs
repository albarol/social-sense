using System.Net;
using SocialSense.Authorization;

namespace SocialSense.Spiders.Behaviors
{
    public class AuthorizationBehavior : Behavior
    {
        private IAuthorization authorization;

        public AuthorizationBehavior(IAuthorization authorization)
        {
            this.authorization = authorization;
        }

        public override void Execute()
        {
            if (authorization.Via == AuthorizationVia.Header)
            {
                this.Spider.HttpWebRequest.Headers.Add("Authorization", this.authorization.Generate());
            }
            else
            {
                var uri = string.Format("{0}&{1}", this.Spider.HttpWebRequest.RequestUri, authorization.Generate());
                this.Spider.HttpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            }
        }
    }
}
