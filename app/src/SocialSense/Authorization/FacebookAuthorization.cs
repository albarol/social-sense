namespace SocialSense.Authorization
{
    public class FacebookAuthorization : IAuthorization
    {
        public string Token { get; set; }
        public AuthorizationVia Via { get { return AuthorizationVia.Url; } }

        public string Generate()
        {
            return string.Format("access_token={0}", Token);
        }
    }
}
