using System;
using System.Security.Cryptography;
using DotNetDoodle.OAuth.Domain.Utility;

namespace DotNetDoodle.OAuth.Domain.Entities
{
    public class Client : IObject, ICreationTrackable, IUpdatesTrackable, IDeleteable
    {
        public Client(string name, OAuthFlow allowedFlow)
        {
            if (name == null) throw new ArgumentNullException("name");

            Id = GenerateKey(name);
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

        public DateTimeOffset CreatedOn { get; protected set; }
        public DateTimeOffset LastUpdatedOn { get; protected set; }

        public DateTimeOffset? DeletedOn { get; private set; }

        public virtual bool HasClientSecret()
        {
            return ClientSecretHash != null;
        }

        public virtual void SetClientSecret(string clientSecret)
        {
            if (clientSecret == null) throw new ArgumentNullException("clientSecret");

            ClientSecretHash = PasswordHasher.HashPassword(clientSecret);
            Updated();
        }

        public virtual bool Verify(string clientSecret)
        {
            if (clientSecret == null) throw new ArgumentNullException("clientSecret");
            if (HasClientSecret() == false)
            {
                throw new InvalidOperationException("Cannot verify the client because it does not have the client secret.");
            }

            return PasswordHasher.VerifyHashedPassword(ClientSecretHash, clientSecret);
        }

        public virtual void ConcentRequired()
        {
            if (RequireConsent == false)
            {
                RequireConsent = true;
                Updated();
            }
        }

        public virtual void ConcentNotRequired()
        {
            if (RequireConsent)
            {
                RequireConsent = false;
                Updated();
            }
        }

        public virtual void Enable()
        {
            if (IsEnabled == false)
            {
                IsEnabled = true;
                Updated();
            }
        }

        public virtual void Disable()
        {
            if (IsEnabled)
            {
                IsEnabled = false;
                Updated();
            }
        }

        public virtual void Delete()
        {
            if (DeletedOn == null)
            {
                DeletedOn = DateTimeOffset.UtcNow;
                Updated();
            }
        }

        // statics

        public static string GenerateKey(string name)
        {
            return Guid.NewGuid().ToString("N");
        }

        // privates

        private void Updated()
        {
            LastUpdatedOn = DateTimeOffset.UtcNow;
        }
    }
}