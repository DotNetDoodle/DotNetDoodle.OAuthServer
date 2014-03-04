
namespace DotNetDoodle.OAuthServer.Infrastructure.Objects
{
    public class Client
    {
        public string Id { get; set; }
        public OAuthFlow Flow { get; set; }
        public bool AllowRefreshToken { get; set; }
        public bool RequireConsent { get; set; }
    }
}