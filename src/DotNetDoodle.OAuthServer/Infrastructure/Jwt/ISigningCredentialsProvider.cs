using System.IdentityModel.Tokens;

namespace DotNetDoodle.OAuthServer.Infrastructure.Jwt
{
    public interface ISigningCredentialsProvider
    {
        SigningCredentials GetSigningCredentials(string issuer, string audiance);
    }
}