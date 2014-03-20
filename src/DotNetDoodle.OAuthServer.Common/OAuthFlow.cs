
namespace DotNetDoodle.OAuthServer.Common
{
    public enum OAuthFlow : byte
    {
        Code = 1,
        Implicit = 2,
        ResourceOwner = 3,
        Client = 4
    }
}
