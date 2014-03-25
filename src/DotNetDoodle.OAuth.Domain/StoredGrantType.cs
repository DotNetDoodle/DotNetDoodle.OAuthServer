
namespace DotNetDoodle.OAuth.Domain
{
    public enum StoredGrantType : byte
    {
        AuthorizationCode = 1,
        RefreshTokenIdentifier = 2,
        ConsentDecision = 3
    }
}