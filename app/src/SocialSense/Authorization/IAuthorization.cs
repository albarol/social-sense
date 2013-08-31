namespace SocialSense.Authorization
{
    public interface IAuthorization
    {
        AuthorizationVia Via { get; }
        string Generate();
    }
}
