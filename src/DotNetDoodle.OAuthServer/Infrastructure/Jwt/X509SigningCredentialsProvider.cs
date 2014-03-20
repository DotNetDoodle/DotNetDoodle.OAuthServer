using System;
using System.IdentityModel.Tokens;

namespace DotNetDoodle.OAuthServer.Infrastructure.Jwt
{
    /// <summary>
    /// Refer to: https://github.com/tugberkugurlu/OwinSamples/blob/master/OwinOAuthSample2/src/OAuth.Server/Providers/X509CertificateSigningCredentialsProvider.cs
    /// </summary>
    public class X509SigningCredentialsProvider : ISigningCredentialsProvider
    {
        public SigningCredentials GetSigningCredentials(string issuer, string audiance)
        {
            throw new NotImplementedException();
        }
    }
}