using System;
using System.IdentityModel.Tokens;

namespace DotNetDoodle.OAuthServer.Infrastructure.Jwt
{
    /// <summary>
    /// Refer to: https://github.com/thinktecture/Thinktecture.IdentityModel/blob/d1b7b00f0d22e3c0fc0f263e3caf643328d95f31/source/Thinktecture.IdentityModel.Core/Tokens/HmacSigningCredentials.cs
    ///           https://github.com/webapibook/WebApiBook.Security/blob/master/src/WebApiBook.Security.Facts/JwtFacts.cs
    /// </summary>
    public class HmacSigningCredentialsProvider : ISigningCredentialsProvider
    {
        private readonly SigningCredentials _defaultSigningCredentials;

        /// <remarks>
        /// For now, we are getting the key from the ctor. Later, this will be retrieved by the audiance.
        /// </remarks>
        public HmacSigningCredentialsProvider(string base64EncodedKey)
            : this(Convert.FromBase64String(base64EncodedKey))
        {
        }

        /// <remarks>
        /// For now, we are getting the key from the ctor. Later, this will be retrieved by the audiance.
        /// </remarks>
        public HmacSigningCredentialsProvider(byte[] key)
        {
            SymmetricSecurityKey symmetricSecurityKey = new InMemorySymmetricSecurityKey(key);
            _defaultSigningCredentials = new SigningCredentials(symmetricSecurityKey, CreateSignatureAlgorithm(key), CreateDigestAlgorithm(key));
        }

        public SigningCredentials GetSigningCredentials(string issuer, string audiance)
        {
            return _defaultSigningCredentials;
        }

        // privates

        private static string CreateSignatureAlgorithm(byte[] key)
        {
            switch (key.Length)
            {
                case 32:
                    return Constants.Algorithms.HmacSha256Signature;
                case 48:
                    return Constants.Algorithms.HmacSha384Signature;
                case 64:
                    return Constants.Algorithms.HmacSha512Signature;
                default:
                    throw new InvalidOperationException("Unsupported key lenght");
            }
        }

        private static string CreateDigestAlgorithm(byte[] key)
        {
            switch (key.Length)
            {
                case 32:
                    return Constants.Algorithms.Sha256Digest;
                case 48:
                    return Constants.Algorithms.Sha384Digest;
                case 64:
                    return Constants.Algorithms.Sha512Digest;
                default:
                    throw new InvalidOperationException("Unsupported key length");
            }
        }
    }
}