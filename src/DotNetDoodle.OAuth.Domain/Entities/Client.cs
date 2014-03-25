using System;
using DotNetDoodle.OAuth.Domain.Utility;

namespace DotNetDoodle.OAuth.Domain.Entities
{
    public class Client
    {
        public Client(string name, OAuthFlow allowedFlow)
        {
            if (name == null) throw new ArgumentNullException("name");

            Id = Guid.NewGuid().ToString("N");
            Name = name;
            AllowedFlow = allowedFlow;
            AllowRefreshToken = (allowedFlow == OAuthFlow.Code);
            RequireConsent = (allowedFlow == OAuthFlow.Code || allowedFlow == OAuthFlow.Implicit);
            IsEnabled = true;
            CreatedOn = DateTimeOffset.UtcNow;
            LastUpdatedOn = DateTimeOffset.UtcNow;
        }

        public string Id { get; protected set; }
        public string Name { get; protected set; }
        public OAuthFlow AllowedFlow { get; protected set; }

        public string ClientSecretHash { get; protected set; }

        /// <summary>
        /// Indicates whether the refresh token should be issued for this client or not.
        /// </summary>
        public bool AllowRefreshToken { get; protected set; }

        /// <remarks>
        /// Only checked if (Flow == Code || Flow == Implicit)
        /// </remarks>
        public bool RequireConsent { get; protected set; }

        /// <summary>
        /// Indicates whether the client is enabled or not.
        /// </summary>
        public bool IsEnabled { get; protected set; }

        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset LastUpdatedOn { get; set; }

        public virtual bool HasClientSecret()
        {
            return ClientSecretHash != null;
        }

        public virtual void SetClientSecret(string clientSecret)
        {
            if (clientSecret == null) throw new ArgumentNullException("clientSecret");

            ClientSecretHash = PasswordHasher.HashPassword(clientSecret);
            LastUpdatedOn = DateTimeOffset.UtcNow;
        }

        public bool Verify(string clientSecret)
        {
            if (clientSecret == null) throw new ArgumentNullException("clientSecret");
            if (HasClientSecret() == false)
            {
                throw new InvalidOperationException("Cannot verify the client because it does not have the client secret.");
            }

            return PasswordHasher.VerifyHashedPassword(ClientSecretHash, clientSecret);
        }

        public virtual void Enable()
        {
            if (IsEnabled == false)
            {
                IsEnabled = true;
                LastUpdatedOn = DateTimeOffset.UtcNow;
            }
        }

        public virtual void Disable()
        {
            if (IsEnabled)
            {
                IsEnabled = false;
                LastUpdatedOn = DateTimeOffset.UtcNow;
            }
        }
    }
}