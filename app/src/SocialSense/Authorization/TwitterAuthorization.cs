namespace SocialSense.Authorization
{
    public class TwitterAuthorization : IAuthorization
    {
        public string ConsumerKey { get; set; }
        public string Nonce { get; set; }
        public string Signature { get; set; }
        public string SignatureMethod { get; set; }
        public string Timestamp { get; set; }
        public string Token { get; set; }
        public string Version { get; set; }

        public AuthorizationVia Via { get { return AuthorizationVia.Header; } }

        public string Generate()
        {
            return string.Format("OAuth oauth_consumer_key=\"{0}\"," +
                                 "oauth_nonce=\"{1}\"," +
                                 "oauth_signature=\"{2}\"," +
                                 "oauth_signature_method=\"{3}\"," +
                                 "oauth_timestamp=\"{4}\"," +
                                 "oauth_token=\"{5}\"," +
                                 "oauth_version=\"{6}",
                                 ConsumerKey, Nonce, Signature, SignatureMethod, Timestamp, Token, Version);
        }
    }
}
