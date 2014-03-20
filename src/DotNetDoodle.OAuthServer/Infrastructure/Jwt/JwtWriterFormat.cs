using System;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using Microsoft.Owin.Security;

namespace DotNetDoodle.OAuthServer.Infrastructure.Jwt
{
    public class JwtWriterFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly ISigningCredentialsProvider _signingCredentialsProvider;
        private readonly TimeSpan _defaultJwtExpireTimeSpan;

        public JwtWriterFormat(ISigningCredentialsProvider signingCredentialsProvider, TimeSpan defaultJwtExpireTimeSpan)
        {
            _signingCredentialsProvider = signingCredentialsProvider;
            _defaultJwtExpireTimeSpan = defaultJwtExpireTimeSpan;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            string issuer;
            string audience;
            data.Properties.Dictionary.TryGetValue(JwtWriterFormatConstants.IssuerKey, out issuer);
            data.Properties.Dictionary.TryGetValue(JwtWriterFormatConstants.AudienceKey, out audience);

            if (issuer == null) throw new InvalidOperationException("AuthenticationTicket.Properties does not include 'Issuer' value.");
            if (audience == null) throw new InvalidOperationException("AuthenticationTicket.Properties does not include 'Audience' value.");

            DateTime issuedUtc = data.Properties.IssuedUtc.HasValue
                ? GetUtcDateTime(data.Properties.IssuedUtc.Value)
                : DateTime.UtcNow;

            DateTime expiresUtc = data.Properties.ExpiresUtc.HasValue
                ? GetUtcDateTime(data.Properties.ExpiresUtc.Value)
                : DateTime.UtcNow.Add(_defaultJwtExpireTimeSpan);

            SigningCredentials signingCredentials = _signingCredentialsProvider.GetSigningCredentials(issuer, audience);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: data.Identity.Claims,
                lifetime: new Lifetime(issuedUtc, expiresUtc),
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }

        // privates

        private DateTime GetUtcDateTime(DateTimeOffset dateTime)
        {
            return new DateTime(
                    dateTime.Year,
                    dateTime.Month,
                    dateTime.Day,
                    dateTime.Hour,
                    dateTime.Minute,
                    dateTime.Second,
                    DateTimeKind.Utc
                );
        }
    }
}